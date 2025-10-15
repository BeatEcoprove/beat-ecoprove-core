using BeatEcoprove.Contracts.Profile.Notifications;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Notifications;

using Mapster;

namespace BeatEcoprove.Api.Mappers;

public class NotificationMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Notification, NotificationResponse>()
            .MapWith(src => new NotificationResponse
                (
                    src.Title,
                    src.Type
                ));

        // Removido: Mapeamento de InviteNotification pois agora é gerenciado pelo microserviço de grupos

        config.NewConfig<LeveUpNotification, LevelUpNotificationResponse>()
            .MapWith(src => new LevelUpNotificationResponse
                (
                    src.Title,
                    src.StagedLevel,
                    src.StagedXp,
                    src.Type
                ));
    }
}