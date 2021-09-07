using System.IO;
using System.Linq;

using Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Server.Data
{
  public class BegehungDbContextFactory : IDesignTimeDbContextFactory<BegehungContext>
  {
    readonly ILogger<BegehungDbContextFactory> _logger;

    public BegehungDbContextFactory(ILogger<BegehungDbContextFactory> logger)
    {
      _logger = logger;
    }

    public BegehungContext CreateDbContext(string[] args)
    {
      var configuration = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .Build();
      var connectionString = args.Any() ? args[0] : configuration.GetConnectionString("Domain");

      _logger.LogDebug("Domain data connection string {ConnectionString}", connectionString);
      var builder = new DbContextOptionsBuilder<BegehungContext>();

      builder.UseMySql(connectionString,
                       ServerVersion.AutoDetect(connectionString),
                       db => db.MigrationsAssembly(typeof(BegehungDbContextFactory)
                                                   .Assembly
                                                   .GetName()
                                                   .Name));

      return new BegehungContext(builder.Options);
    }
  }
}
