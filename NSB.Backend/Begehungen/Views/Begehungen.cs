using System.Threading.Tasks;

using Infrastructure;

using NSB.Backend.Begehungen.Events;

using NServiceBus;

namespace NSB.Backend.Begehungen.Views
{
  public class Begehungen : IHandleMessages<BegehungGestartet>
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
  }
}
