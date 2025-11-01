using System.Globalization;

using BeatEcoprove.Domain.Shared.Broker;

namespace BeatEcoprove.Infrastructure.EmailSender;

public record EmailQueueEvent(
    string Recipient,
    string Template,
    Dictionary<string, string> Variables
) : IEmailEvent
{
    public static Guid Id => Guid.NewGuid();

    public static string SendAt => DateTime.UtcNow.ToString(
        "yyyy-MM-ddTHH:mm:ss.fffffffK",
        CultureInfo.InvariantCulture
    );
}