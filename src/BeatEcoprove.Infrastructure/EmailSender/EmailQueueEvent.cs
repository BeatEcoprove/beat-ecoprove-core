using System.Globalization;

using BeatEcoprove.Domain.Shared.Broker;

namespace BeatEcoprove.Infrastructure.EmailSender;

public record EmailQueueEvent(
    string Recipient, 
    string Template,
    Dictionary<string, string> Variables
) : IEmailEvent  {
    public Guid Id => Guid.NewGuid();
    
    public string SendAt => DateTime.UtcNow.ToString(
        "yyyy-MM-ddTHH:mm:ss.fffffffK",
        CultureInfo.InvariantCulture
    );
}