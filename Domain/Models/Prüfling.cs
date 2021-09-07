namespace Domain.Models
{
  public class Prüfling
  {
    public string Id { get; set; } = default!;
    public Prüflingstyp Typ { get; set; } = default!;
    public string Bezeichnung { get; set; } = default!;
    public string Straße { get; set; } = default!;
    public string Haus { get; set; } = default!;
    public string Ort { get; set; } = default!;
  }

  public enum Prüflingstyp
  {
    Raum,
    TechnischeAnlage
  }
}
