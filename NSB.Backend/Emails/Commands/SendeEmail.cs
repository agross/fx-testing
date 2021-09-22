namespace NSB.Backend.Emails.Commands
{
  public class SendeEmail
  {
    public string Recipient { get; }

    public SendeEmail(string recipient)
    {
      Recipient = recipient;
    }
  }
}
