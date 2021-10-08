namespace Domain.Models
{
  public class Begehung
  {
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public Begehungsstatus Status { get; set; }
  }

  public enum Begehungsstatus
  {
    Planung,
    Durchführung,
    Abgeschlossen,
    Verworfen,
  }
}
