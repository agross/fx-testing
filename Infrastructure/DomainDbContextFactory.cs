using System;
using System.IO;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
  public class DomainDbContextFactory : IDesignTimeDbContextFactory<DomainDbContext>
  {
    public DomainDbContext CreateDbContext(string[] args)
    {
      var configuration = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json")
                          .Build();
      var connectionString = args.Any() ? args[0] : configuration.GetConnectionString("Domain");

      Console.WriteLine($"Domain data connection string ${connectionString}");
      var builder = new DbContextOptionsBuilder<DomainDbContext>();

      builder.UseMySql(connectionString,
                       ServerVersion.AutoDetect(connectionString),
                       db => db.MigrationsAssembly(GetType()
                                                   .Assembly
                                                   .GetName()
                                                   .Name));

      return new DomainDbContext(builder.Options);
    }
  }
}
