// using System;
// using System.Linq.Expressions;
// using System.Runtime.Serialization;
// using System.Threading.Tasks;
//
// using Domain.Models;
//
// using MassTransit;
// using MassTransit.Saga;
//
// using MT.Backend.Begehungen.Events;
// using MT.Backend.Begehungen.Messages;
// using MT.Backend.Emails.Commands;
// using MT.Backend.Messages.Begehungen;
//
// namespace MT.Backend.Begehungen
// {
//   public class BegehungSaga : ISaga,
//                               InitiatedBy<StarteBegehung>,
//                               Orchestrates<SchließeBegehungAb>,
//                               Orchestrates<VerwerfeBegehung>,
//                               Orchestrates<Erinnern>,
//
//                               // Does not work.
//                               Observes<SchließeBegehungAb, BegehungSaga>,
//                               Observes<VerwerfeBegehung, BegehungSaga>
//   {
//     public Guid CorrelationId { get; set; }
//     public Begehungsstatus Status;
//     public string BegehungId { get; set; }
//
//     [IgnoreDataMember]
//     Expression<Func<BegehungSaga, SchließeBegehungAb, bool>> Observes<SchließeBegehungAb, BegehungSaga>.
//       CorrelationExpression =>
//       (saga, message) => saga.BegehungId == message.BegehungId;
//
//     [IgnoreDataMember]
//     public Expression<Func<BegehungSaga, VerwerfeBegehung, bool>> CorrelationExpression =>
//       (saga, message) => saga.BegehungId == message.BegehungId;
//
//     public async Task Consume(ConsumeContext<StarteBegehung> context)
//     {
//       CorrelationId = InVar.CorrelationId;
//       BegehungId = context.Message.BegehungId;
//
//       if (Status != Begehungsstatus.Planung &&
//           Status != Begehungsstatus.Durchführung)
//       {
//         throw new Exception();
//       }
//
//       Status = Begehungsstatus.Durchführung;
//
//       await context.Publish<BegehungGestartet>(new { Id = BegehungId });
//
//       await context.ScheduleSend<Erinnern>(TimeSpan.FromSeconds(30), new { InVar.CorrelationId });
//     }
//
//     public async Task Consume(ConsumeContext<SchließeBegehungAb> context)
//     {
//       if (Status != Begehungsstatus.Durchführung &&
//           Status != Begehungsstatus.Abgeschlossen)
//       {
//         throw new Exception();
//       }
//
//       Status = Begehungsstatus.Abgeschlossen;
//
//       await context.Publish<BegehungAbgeschlossen>(new { Id = BegehungId });
//       await context.Send<SendeEmail>(new
//       {
//         Recipient = "begeher@example.com",
//
//         Subject = $"Begehung {BegehungId} abgeschlossen",
//         Body = "Danke.",
//       });
//
//       await ((SagaConsumeContext<BegehungSaga, SchließeBegehungAb>) context).SetCompleted();
//     }
//
//     public async Task Consume(ConsumeContext<VerwerfeBegehung> context)
//     {
//       Status = Begehungsstatus.Verworfen;
//
//       await context.Publish<BegehungVerworfen>(new { Id = BegehungId });
//       await context.Send<SendeEmail>(new
//       {
//         Recipient = "begeher@example.com",
//
//         Subject = $"Begehung {BegehungId} verworfen",
//         Body = "Danke.",
//       });
//
//       await ((SagaConsumeContext<BegehungSaga, VerwerfeBegehung>) context).SetCompleted();
//     }
//
//     public async Task Consume(ConsumeContext<Erinnern> context)
//     {
//       if (Status != Begehungsstatus.Durchführung)
//       {
//         return;
//       }
//
//       await context.Send<SendeEmail>(new
//       {
//         Recipient = "begeher@example.com",
//
//         Subject = $"Begehung {BegehungId} ist offen",
//         Body = "Bitte kümmern Sie sich.",
//       });
//
//       await context.ScheduleSend<Erinnern>(TimeSpan.FromSeconds(30), new { });
//     }
//   }
// }
