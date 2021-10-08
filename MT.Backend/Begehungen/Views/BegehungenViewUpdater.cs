using System.Threading.Tasks;

using Domain.Models;

using Infrastructure;

using MassTransit;

using MT.Backend.Begehungen.Events;

namespace MT.Backend.Begehungen.Views
{
  public class BegehungenViewUpdater : IConsumer<BegehungGestartet>,
                                       IConsumer<BegehungAbgeschlossen>,
                                       IConsumer<BegehungVerworfen>
  {
    readonly DomainDbContext _db;

    public BegehungenViewUpdater(DomainDbContext db)
    {
      _db = db;
    }

    public async Task Consume(ConsumeContext<BegehungGestartet> context)
    {
      var begehung = await _db.Begehungen.FindAsync(context.Message.Id);
      begehung.Status = Begehungsstatus.Durchführung;
      await _db.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<BegehungAbgeschlossen> context)
    {
      var begehung = await _db.Begehungen.FindAsync(context.Message.Id);
      begehung.Status = Begehungsstatus.Abgeschlossen;
      await _db.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<BegehungVerworfen> context)
    {
      var begehung = await _db.Begehungen.FindAsync(context.Message.Id);
      begehung.Status = Begehungsstatus.Verworfen;
      await _db.SaveChangesAsync();
    }
  }
}
