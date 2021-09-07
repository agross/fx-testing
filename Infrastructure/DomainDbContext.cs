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
  }
}
