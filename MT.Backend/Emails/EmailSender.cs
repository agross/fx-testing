using System;
using System.Threading.Tasks;

using MailKit.Net.Smtp;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MimeKit;

using MT.Backend.Emails.Commands;

namespace MT.Backend.Emails
{
  // Must be public.
  public class EmailSender : IConsumer<SendeEmail>
  {
    readonly ILogger<EmailSender> _logger;
    readonly IConfigurationSection _smtp;

    public EmailSender(ILogger<EmailSender> logger, IConfiguration config)
    {
      _logger = logger;
      _smtp = config.GetSection("smtp");
    }

    public Task Consume(ConsumeContext<SendeEmail> context)
    {
      _logger.LogInformation("Sende E-Mail an {Recipient}", context.Message.Recipient);

      var mail = new MimeMessage();
      mail.From.Add(new MailboxAddress(_smtp["DefaultSender"], _smtp["DefaultSender"]));
      mail.To.Add(new MailboxAddress(context.Message.Recipient, context.Message.Recipient));
      mail.Subject = context.Message.Subject;

      var builder = new BodyBuilder { TextBody = context.Message.Body };

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
