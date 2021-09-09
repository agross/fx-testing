using System.Threading;
using System.Threading.Tasks;

using Domain.Models;

using Elsa.Activities.Http.Models;
using Elsa.Services;
using Elsa.Services.Models;

using Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Server.Providers.WorkflowContexts
{
  public class Begehung : WorkflowContextRefresher<Domain.Models.Begehung>
  {
    readonly IDbContextFactory<DomainDbContext> _factory;

    public Begehung(IDbContextFactory<DomainDbContext> factory)
    {
      _factory = factory;
    }

    public override async ValueTask<Domain.Models.Begehung?> LoadAsync(LoadWorkflowContext context,
                                                                       CancellationToken cancellationToken = default)
    {
      var contextId = context.ContextId;

      if (contextId == null &&
          context.WorkflowExecutionContext.IsFirstPass)
      {
        var start = ((HttpRequestModel) context.WorkflowExecutionContext.Input!).GetBody<StarteBegehung>();
        contextId = start.BegehungId;
      }

      await using var dbContext = _factory.CreateDbContext();

      return await dbContext.Begehungen
                            .AsQueryable()
                            .FirstOrDefaultAsync(x => x.Id == contextId,
                                                 cancellationToken);
    }

    public override async ValueTask<string?> SaveAsync(SaveWorkflowContext<Domain.Models.Begehung> context,
                                                       CancellationToken cancellationToken = default)
    {
      var begehung = context.Context;
      if (begehung == null)
      {
        return null;
      }

      await using var dbContext = _factory.CreateDbContext();
      var dbSet = dbContext.Begehungen;

      // Required?
      // context.WorkflowExecutionContext.WorkflowContext = begehung;
      // context.WorkflowExecutionContext.ContextId = begehung.Id;

      var existing = await dbSet.AsQueryable()
                                .SingleAsync(x => x.Id == begehung.Id,
                                             cancellationToken);

      dbContext.Entry(existing).CurrentValues.SetValues(begehung);

      await dbContext.SaveChangesAsync(cancellationToken);

      return begehung.Id;
    }
  }
}
