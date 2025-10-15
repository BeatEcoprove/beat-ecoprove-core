using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Notifications;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;

using MongoDB.Driver;

using MessageId = BeatEcoprove.Domain.ProfileAggregator.ValueObjects.MessageId;

namespace BeatEcoprove.Infrastructure.Persistence.Repositories;

public class NotificationRepository : DocumentRepository<Notification, MessageId>, INotificationRepository
{
    public NotificationRepository(IMongoDatabase database)
        : base(database)
    {
    }

    public async Task<List<Notification>> GetAllNotificationByOwnerIdAsync(ProfileId ownerId, CancellationToken cancellationToken = default)
    {
        var filter = Builders<Notification>.Filter.And(
            Builders<Notification>.Filter.Eq("Owner", ownerId),
            Builders<Notification>.Filter.Eq("DeletedAt", 0));

        return await Collection
            .Find(filter)
            .ToListAsync(cancellationToken);
    }

    // Removido: Funcionalidade de convites agora é gerida pelo microserviço de grupos
}