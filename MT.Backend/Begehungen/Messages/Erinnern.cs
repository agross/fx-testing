using System;

using MassTransit;

namespace MT.Backend.Begehungen.Messages
{
  public interface Erinnern : CorrelatedBy<Guid>
  {
    public string Id { get; set; }
  }
}
