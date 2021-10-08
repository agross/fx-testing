namespace MT.Backend.Emails.Commands
{
  public interface SendeEmail
  {
    public string Recipient { get; }
    public string Subject { get; set; }
    public string Body { get; set; }
  }
}
