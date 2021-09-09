namespace Domain.Models
{
  public class StarteBegehung
  {
    public string BegehungId { get; set; } = default!;
  }

  // For backward compat with older workflow versions.
  public class BegehungStart : StarteBegehung
  {
  }
}
