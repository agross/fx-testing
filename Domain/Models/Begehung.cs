using System;

namespace Domain.Models
{
  public class Begehung
  {
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public Begehungsstatus Status { get; private set; }

    public void Starten()
    {
      if (Status != Begehungsstatus.Planung &&
          Status != Begehungsstatus.Durchführung)
      {
        throw new BegehungNichtInPlanungException();
      }

      Status = Begehungsstatus.Durchführung;
    }

    public void Abschließen()
    {
      if (Status != Begehungsstatus.Durchführung &&
          Status != Begehungsstatus.Abgeschlossen)
      {
        throw new BegehungNichtInDurchführungException();
      }

      Status = Begehungsstatus.Abgeschlossen;
    }

    public void Verwerfen()
    {
      Status = Begehungsstatus.Verworfen;
    }
  }

  public class BegehungNichtInDurchführungException : Exception
  {
  }

  public class BegehungNichtInPlanungException : Exception
  {
  }

  public enum Begehungsstatus
  {
    Planung,
    Durchführung,
    Abgeschlossen,
    Verworfen,
  }
}
