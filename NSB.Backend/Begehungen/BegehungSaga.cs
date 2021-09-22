using System;
using System.Threading.Tasks;

using Domain.Models;

using NSB.Backend.Begehungen.Commands;
using NSB.Backend.Begehungen.Events;
using NSB.Backend.Begehungen.Messages;
using NSB.Backend.Emails.Commands;

using NServiceBus;

namespace NSB.Backend.Begehungen
{
  public class BegehungSaga : Saga<Data>,
                              IAmStartedByMessages<StarteBegehung>,
                              IHandleMessages<BegehungAbschließen>,
                              IHandleMessages<BegehungVerwerfen>,
                              IHandleTimeouts<Erinnern>
  {
    public async Task Handle(StarteBegehung message, IMessageHandlerContext context)
    {
      Data.BegehungId = message.BegehungId;

      if (Data.Status != Begehungsstatus.Planung &&
          Data.Status != Begehungsstatus.Durchführung)
      {
        throw new BegehungNichtInPlanungException();
      }

      Data.Status = Begehungsstatus.Durchführung;

      await context.Publish(new BegehungGestartet { Id = Data.BegehungId });
      await RequestTimeout<Erinnern>(context, TimeSpan.FromSeconds(30));
    }

    public Task Handle(BegehungAbschließen message, IMessageHandlerContext context)
    {
      if (Data.Status != Begehungsstatus.Durchführung &&
          Data.Status != Begehungsstatus.Abgeschlossen)
      {
        throw new BegehungNichtInDurchführungException();
      }

      Data.Status = Begehungsstatus.Abgeschlossen;

      return Task.CompletedTask;
    }

    public Task Handle(BegehungVerwerfen message, IMessageHandlerContext context)
    {
      Data.Status = Begehungsstatus.Verworfen;
      MarkAsComplete();

      return Task.CompletedTask;
    }

    public async Task Timeout(Erinnern message, IMessageHandlerContext context)
    {
      if (Data.Status != Begehungsstatus.Durchführung)
      {
        return;
      }

      await context.SendLocal(new SendeEmail("begeher@example.com"));
      await RequestTimeout<Erinnern>(context, TimeSpan.FromSeconds(30));
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<Data> mapper)
    {
      mapper.MapSaga(s => s.BegehungId)
            .ToMessage<StarteBegehung>(m => m.BegehungId)
            .ToMessage<BegehungAbschließen>(m => m.BegehungId)
            .ToMessage<BegehungVerwerfen>(m => m.BegehungId);
    }
  }
}
