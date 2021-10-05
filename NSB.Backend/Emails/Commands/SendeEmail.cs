namespace NSB.Backend.Emails.Commands
{
  public class SendeEmail
  {
    public string Recipient { get; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public SendeEmail(string recipient)
    {
      Recipient = recipient;
    }
  }
}
