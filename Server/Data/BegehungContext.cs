using Microsoft.EntityFrameworkCore;

using Server.Models;

namespace Server.Data
{
    public class BegehungContext : DbContext
    {
        public BegehungContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Begehung> Begehungen { get; set; } = default!;
    }
}