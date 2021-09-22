using System;

using Domain.Models;

using NServiceBus;

namespace NSB.Backend.Begehungen
{
  public class Data : IContainSagaData
  {
    public Begehungsstatus Status;
    public string BegehungId { get; set; }
    public Guid Id { get; set; }
    public string Originator { get; set; }
    public string OriginalMessageId { get; set; }
  }
}
