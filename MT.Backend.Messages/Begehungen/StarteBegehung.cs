using System;

using MassTransit;

namespace MT.Backend.Messages.Begehungen
{
  public interface StarteBegehung : CorrelatedBy<Guid>
  {
    public string BegehungId { get; set; }
  }
}
