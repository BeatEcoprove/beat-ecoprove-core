using BeatEcoprove.Application.Shared.Interfaces.Helpers;
using BeatEcoprove.Application.Shared.Interfaces.Providers;
using BeatEcoprove.Domain.Shared.Broker;

using MassTransit;

namespace BeatEcoprove.Infrastructure.EmailSender;

public class MailSender(
    ITopicProducer<IEmailEvent> emailPublisher
) : IMailSender
{
    private static readonly List<Mail> Mails = [];

    public async Task SendMailAsync(
        Mail mail,
        CancellationToken cancellationToken = default)
    {
        await emailPublisher.Produce(
            new EmailQueueEvent(
                mail.To,
                mail.Template,
                mail.Variables
            ),
            cancellationToken);

        Mails.Add(mail);
    }

    public Mail? Last()
        => Mails.Count == 0 ? null : Mails.Last();
}