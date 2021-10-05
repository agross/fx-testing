using System.Threading.Tasks;

using Infrastructure;

using NSB.Backend.Begehungen.Events;

using NServiceBus;

namespace NSB.Backend.Begehungen.Views
{
  public class Begehungen : IHandleMessages<BegehungGestartet>,
                            IHandleMessages<BegehungAbgeschlossen>,
                            IHandleMessages<BegehungVerworfen>
  {
    readonly DomainDbContext _db;

    public Begehungen(DomainDbContext db)
    {
      _db = db;
    }

    public async Task Handle(BegehungGestartet message, IMessageHandlerContext context)
    {
      var begehung = await _db.Begehungen.FindAsync(message.Id);
      begehung.Starten();
      await _db.SaveChangesAsync();
    }

    public async Task Handle(BegehungAbgeschlossen message, IMessageHandlerContext context)
    {
      var begehung = await _db.Begehungen.FindAsync(message.Id);
      begehung.Abschließen();
      await _db.SaveChangesAsync();
    } 
    
    public async Task Handle(BegehungVerworfen message, IMessageHandlerContext context)
    {
      var begehung = await _db.Begehungen.FindAsync(message.Id);
      begehung.Verwerfen();
      await _db.SaveChangesAsync();
    }
  }
}
