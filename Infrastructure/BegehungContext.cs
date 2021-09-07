using Domain.Models;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
  public class BegehungContext : DbContext
  {
    public BegehungContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Begehung> Begehungen { get; set; } = default!;
  }
}
