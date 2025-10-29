using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Helpers;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Application.Shared.Interfaces.Services.Common;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.StoreAggregator.Daos;
using BeatEcoprove.Domain.StoreAggregator.Entities;
using BeatEcoprove.Domain.StoreAggregator.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.Stores.Commands.AddWorker;

internal sealed class AddWorkerCommandHandler(
    IProfileManager profileManager,
    IStoreService storeService,
    IStoreRepository storeRepository,
    IMailSender mailSender,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddWorkerCommand, ErrorOr<WorkerDao>>
{
    public async Task<ErrorOr<WorkerDao>> Handle(AddWorkerCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var storeId = StoreId.Create(request.StoreId);

        var workerType = storeService.GetWorkerType(request.Permission);

        if (workerType.IsError)
        {
            return workerType.Errors;
        }

        var email = Email.Create(request.Email);

        if (email.IsError)
        {
            return email.Errors;
        }

        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var store = await storeRepository.GetByIdAsync(storeId, cancellationToken);

        if (store is null)
        {
            return Errors.Store.StoreNotFound;
        }

        var result = await storeService.RegisterWorkerAsync(
            store,
            profile.Value,
            new AddWorkerInput(
                request.Name,
                email.Value,
                workerType.Value
            ),
            cancellationToken
        );

        if (result.IsError)
        {
            return result.Errors;
        }

        (Worker worker, Password password) = result.Value;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var workerDao = await storeRepository.GetWorkerDaoAsync(worker.Id, cancellationToken);

        if (workerDao is null)
        {
            return Errors.Worker.NotAllowedName;
        }

        await mailSender.SendMailAsync(
            new Mail(
                email.Value,
                "delivery-employee-key",
                Variables: new Dictionary<string, string>()
                {
                    {"password", password.Value}
                }
            ),
            cancellationToken
        );

        return workerDao;
    }
}