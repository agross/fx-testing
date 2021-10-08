using System;

using MassTransit;

namespace MT.Backend.Messages.Begehungen
{
  public interface SchließeBegehungAb : CorrelatedBy<Guid>
  {
    public string BegehungId { get; set; }
  }
}
