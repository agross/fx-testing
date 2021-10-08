using System;

using MassTransit;

namespace MT.Backend.Messages.Begehungen
{
  public interface VerwerfeBegehung : CorrelatedBy<Guid>
  {
    public string BegehungId { get; set; }
  }
}
