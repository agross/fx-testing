using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using NSB.Backend.Emails.Commands;

using NServiceBus;

namespace NSB.Backend.Emails
{
  class EmailSender : IHandleMessages<SendeEmail>
  {
    readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
      _logger = logger;
    }

    public Task Handle(SendeEmail message, IMessageHandlerContext context)
    {
      _logger.LogInformation("Sende E-Mail an {Recipient}", message.Recipient);

      return Task.CompletedTask;
    }
  }
}
