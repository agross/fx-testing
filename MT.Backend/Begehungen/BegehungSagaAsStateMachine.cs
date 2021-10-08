using System;

using Automatonymous;

using Marten.Schema;

using MassTransit;

using MT.Backend.Begehungen.Events;
using MT.Backend.Begehungen.Messages;
using MT.Backend.Emails.Commands;
using MT.Backend.Messages.Begehungen;

namespace MT.Backend.Begehungen
{
  public class Begehungen :
    SagaStateMachineInstance
  {
    [Identity]
    public Guid CorrelationId { get; set; }

    public int Status { get; set; }
    public string BegehungId { get; set; }
    public Guid? ErinnernTimeoutTokenId { get; set; }
  }

  public class BegehungSagaAsStateMachine : MassTransitStateMachine<Begehungen>
  {
    public BegehungSagaAsStateMachine()
    {
      InstanceState(x => x.Status, Eingereicht, Durchführung);

      Event(() => StarteBegehung,
            e => e
                 .CorrelateBy((instance, context) => instance.BegehungId == context.Message.BegehungId)
                 .SelectId(x => NewId.NextGuid()));
      Event(() => SchließeBegehungAb,
            e => e
              .CorrelateBy((instance, context) => instance.BegehungId == context.Message.BegehungId)
           );
      Event(() => VerwerfeBegehung,
            e => e
              .CorrelateBy((instance, context) => instance.BegehungId == context.Message.BegehungId)
           );

      Schedule(() => ErinnernTimeout,
               instance => instance.ErinnernTimeoutTokenId,
               s =>
               {
                 s.Delay = TimeSpan.FromSeconds(30);

                 s.Received = r => r.CorrelateBy((instance, context) => instance.BegehungId == context.Message.Id);
               });

      Initially(
                When(StarteBegehung)
                  .Then(x => x.Instance.BegehungId = x.Data.BegehungId)
                  .Schedule(ErinnernTimeout,
                            context => context.Init<Erinnern>(new { Id = context.Data.BegehungId }))
                  .PublishAsync(x => x.Init<BegehungGestartet>(new { Id = x.Data.BegehungId }))
                  .TransitionTo(Durchführung));

      During(Durchführung,
             When(ErinnernTimeout.Received)
               .SendAsync(context => context.Init<SendeEmail>(new
               {
                 Recipient = "begeher@example.com",
                 Subject = $"Begehung {context.InstanceContext.Instance.BegehungId} ist in Durchführung",
                 Body = "Bitte kümmern Sie sich.",
               }))
               .Schedule(ErinnernTimeout,
                         context => context.Init<Erinnern>(new { context.Data.Id })),
             When(SchließeBegehungAb)
               .SendAsync(context => context.Init<SendeEmail>(new
               {
                 Recipient = "begeher@example.com",
                 Subject = $"Begehung {context.InstanceContext.Instance.BegehungId} abgeschlossen",
                 Body = "Danke.",
               }))
               .PublishAsync(x => x.Init<BegehungAbgeschlossen>(new { Id = x.Data.BegehungId }))
               .Unschedule(ErinnernTimeout)
               .TransitionTo(Final),
             When(VerwerfeBegehung)
               .SendAsync(context => context.Init<SendeEmail>(new
               {
                 Recipient = "begeher@example.com",
                 Subject = $"Begehung {context.InstanceContext.Instance.BegehungId} verworfen",
                 Body = "Danke.",
               }))
               .PublishAsync(x => x.Init<BegehungVerworfen>(new { Id = x.Data.BegehungId }))
               .Unschedule(ErinnernTimeout)
               .TransitionTo(Final));

      // Delete when in Final state.
      // SetCompletedWhenFinalized();
    }

    public State Eingereicht { get; private set; }
    public State Durchführung { get; private set; }
    public Event<StarteBegehung> StarteBegehung { get; private set; }
    public Event<SchließeBegehungAb> SchließeBegehungAb { get; private set; }
    public Event<VerwerfeBegehung> VerwerfeBegehung { get; private set; }
    public Schedule<Begehungen, Erinnern> ErinnernTimeout { get; private set; }
  }
}
