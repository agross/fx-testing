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

    public async Task Handle(BegehungAbschließen message, IMessageHandlerContext context)
    {
      if (Data.Status != Begehungsstatus.Durchführung &&
          Data.Status != Begehungsstatus.Abgeschlossen)
      {
        throw new BegehungNichtInDurchführungException();
      }

      Data.Status = Begehungsstatus.Abgeschlossen;
      
      await context.Publish(new BegehungAbgeschlossen { Id = Data.BegehungId });
      await context.SendLocal(new SendeEmail("begeher@example.com")
      {
        Subject = $"Begehung {Data.BegehungId} abgeschlossen",
        Body = "Danke.",
      });
      
      MarkAsComplete();
    }

    public async Task Handle(BegehungVerwerfen message, IMessageHandlerContext context)
    {
      Data.Status = Begehungsstatus.Verworfen;
      
      await context.Publish(new BegehungVerworfen { Id = Data.BegehungId });
      await context.SendLocal(new SendeEmail("begeher@example.com")
      {
        Subject = $"Begehung {Data.BegehungId} verworfen",
        Body = "Danke.",
      });
      
      MarkAsComplete();
    }

    public async Task Timeout(Erinnern message, IMessageHandlerContext context)
    {
      if (Data.Status != Begehungsstatus.Durchführung)
      {
        return;
      }

      await context.SendLocal(new SendeEmail("begeher@example.com")
      {
        Subject = $"Begehung {Data.BegehungId} ist offen",
        Body = "Bitte kümmern Sie sich.",
      });
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
