using System;
using System.Threading.Tasks;

using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MimeKit;

using NSB.Backend.Emails.Commands;

using NServiceBus;

namespace NSB.Backend.Emails
{
  class EmailSender : IHandleMessages<SendeEmail>
  {
    readonly ILogger<EmailSender> _logger;
    readonly IConfigurationSection _smtp;

    public EmailSender(ILogger<EmailSender> logger, IConfiguration config)
    {
      _logger = logger;
      _smtp = config.GetSection("smtp");
    }

    public Task Handle(SendeEmail message, IMessageHandlerContext context)
    {
      _logger.LogInformation("Sende E-Mail an {Recipient}", message.Recipient);

      var mail = new MimeMessage();
      mail.From.Add(new MailboxAddress(_smtp["DefaultSender"], _smtp["DefaultSender"]));
      mail.To.Add(new MailboxAddress(message.Recipient, message.Recipient));
      mail.Subject = message.Subject;

      var builder = new BodyBuilder { TextBody = message.Body };

      mail.Body = builder.ToMessageBody();
      mail.Prepare(EncodingConstraint.SevenBit);

      using var client = new SmtpClient();

      client.Connect(_smtp["Host"], Convert.ToInt32(_smtp["Port"]));
      if (!string.IsNullOrWhiteSpace(_smtp["Login"]))
      {
        client.Authenticate(_smtp["Login"], _smtp["Password"]);
      }

      client.Send(mail);
      client.Disconnect(true);

      return Task.CompletedTask;
    }
  }
}
