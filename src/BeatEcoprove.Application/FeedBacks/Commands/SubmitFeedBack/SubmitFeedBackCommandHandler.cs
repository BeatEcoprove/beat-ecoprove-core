using BeatEcoprove.Application.FeedBacks.Commands.Common;
using BeatEcoprove.Application.Shared;
using BeatEcoprove.Application.Shared.Interfaces.Persistence;
using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Application.Shared.Interfaces.Services;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Entities;
using BeatEcoprove.Domain.Shared.ValueObjects;

using ErrorOr;

namespace BeatEcoprove.Application.FeedBacks.Commands.SubmitFeedBack;

internal sealed class SubmitFeedBackCommandHandler(
    IProfileManager profileManager,
    IFeedBackRepository feedBackRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<SubmitFeedBackCommand, ErrorOr<FeedBackResult>>
{
    public async Task<ErrorOr<FeedBackResult>> Handle(SubmitFeedBackCommand request, CancellationToken cancellationToken)
    {
        var profileId = ProfileId.Create(request.ProfileId);
        var title = Title.Create(request.Title);

        if (title.IsError)
        {
            return title.Errors;
        }

        var profile = await profileManager.GetProfileAsync(profileId, cancellationToken);

        if (profile.IsError)
        {
            return profile.Errors;
        }

        var feedBack = FeedBack.Create(
            profile.Value.Id,
            title.Value,
            request.Description);

        await feedBackRepository.AddAsync(feedBack.Value, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new FeedBackResult(
            feedBack.Value.Id,
            profile.Value,
            feedBack.Value.Title,
            feedBack.Value.Description
        );
    }
}