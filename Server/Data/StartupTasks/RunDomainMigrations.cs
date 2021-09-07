using System.Threading;
using System.Threading.Tasks;

using Elsa.Services;

using Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Server.Data.StartupTasks
{
  public class RunDomainMigrations : IStartupTask
  {
    readonly IDbContextFactory<BegehungContext> _dbContextFactory;

    public RunDomainMigrations(IDbContextFactory<BegehungContext> dbContextFactory)
    {
      _dbContextFactory = dbContextFactory;
    }

    public int Order => 0;

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
      await using var dbContext = _dbContextFactory.CreateDbContext();

      await dbContext.Database.MigrateAsync(cancellationToken);

      await dbContext.DisposeAsync();
    }
  }
}
