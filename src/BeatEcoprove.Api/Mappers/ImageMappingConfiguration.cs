using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.ImageUpload.Commands;
using BeatEcoprove.Contracts.ImageUpload;
using BeatEcoprove.Contracts.Services;
using BeatEcoprove.Domain.ClosetAggregator.Entities;

using Mapster;

namespace BeatEcoprove.Api.Mappers;

public class ImageMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UploadedUrl, ImageUploadResponse>()
            .MapWith(src => new ImageUploadResponse(
                src.Url
            ));

        config.NewConfig<MaintenanceService, MaintenanceServiceResponse>()
            .MapWith(src => new MaintenanceServiceResponse(
                src.Id,
                src.Title,
                src.Badge,
                src.Description,
                src.MaintenanceActions.Adapt<List<MaintenanceActionResponse>>()
            ));
    }
}