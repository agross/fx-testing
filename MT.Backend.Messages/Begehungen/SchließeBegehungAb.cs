using System;

using MassTransit;

namespace MT.Backend.Messages.Begehungen
{
  public interface Schlie├čeBegehungAb : CorrelatedBy<Guid>
  {
    public string BegehungId { get; set; }
  }
}
