using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
  public class DomainDbContext : DbContext
  {
    public DomainDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Begehung> Begehungen { get; set; } = default!;
    public DbSet<Begehungsobjekt> Begehungsobjekt { get; set; } = default!;
    public DbSet<Mitarbeiter> Mitarbeiter { get; set; } = default!;
    public DbSet<Prüfling> Prüfling { get; set; } = default!;
  }
}
