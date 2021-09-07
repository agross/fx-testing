using System;
using System.Threading;
using System.Threading.Tasks;

using Elsa.Activities.Http.Models;
using Elsa.Services;
using Elsa.Services.Models;

using Infrastructure;

using Microsoft.EntityFrameworkCore;

using Server.Data;

namespace Server.Providers.WorkflowContexts
{
  public class Begehung : WorkflowContextRefresher<Domain.Models.Begehung>
  {
    readonly IDbContextFactory<BegehungContext> _factory;

    public Begehung(IDbContextFactory<BegehungContext> factory)
    {
      _factory = factory;
    }

    public override async ValueTask<Domain.Models.Begehung?> LoadAsync(LoadWorkflowContext context,
                                                                       CancellationToken cancellationToken = default)
    {
      var contextId = context.ContextId;
      await using var dbContext = _factory.CreateDbContext();

      return await dbContext.Begehungen.AsQueryable()
                            .FirstOrDefaultAsync(x => x.Id == contextId,
                                                 cancellationToken);
    }

    public override async ValueTask<string?> SaveAsync(SaveWorkflowContext<Domain.Models.Begehung> context,
                                                       CancellationToken cancellationToken = default)
    {
      var begehung = context.Context;
      await using var dbContext = _factory.CreateDbContext();
      var dbSet = dbContext.Begehungen;

      if (begehung == null)
      {
        // We are handling a newly posted blog post.
        begehung = ((HttpRequestModel) context.WorkflowExecutionContext.Input!).GetBody<Domain.Models.Begehung>();

        begehung.Id = Guid.NewGuid().ToString("N");

        context.WorkflowExecutionContext.WorkflowContext = begehung;
        context.WorkflowExecutionContext.ContextId = begehung.Id;

        await dbSet.AddAsync(begehung, cancellationToken);
      }
      else
      {
        var existing = await dbSet.AsQueryable()
                                  .SingleAsync(x => x.Id == begehung.Id,
                                               cancellationToken);

        dbContext.Entry(existing).CurrentValues.SetValues(begehung);
      }

      await dbContext.SaveChangesAsync(cancellationToken);

      return begehung.Id;
    }
  }
}
