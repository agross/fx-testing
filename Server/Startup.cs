using System;

using Elsa;
using Elsa.Activities.Temporal;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.MySql;
using Elsa.Runtime;

using Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NodaTime;

using Server.Data.StartupTasks;
using Server.Providers.WorkflowContexts;

namespace Server
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var elsaConnectionString = Configuration.GetConnectionString("Elsa");
      Console.WriteLine($"Elsa database connection: {elsaConnectionString}");

      var domainConnectionString = Configuration.GetConnectionString("Domain");
      Console.WriteLine($"Domain database connection: {domainConnectionString}");

      var elsaSection = Configuration.GetSection("Elsa");

      services
        .AddDbContextFactory
          <DomainDbContext>(b => b.UseMySql(domainConnectionString,
                                            ServerVersion.AutoDetect(domainConnectionString),
                                            o => o.MigrationsAssembly(typeof(DomainDbContext)
                                                                      .Assembly
                                                                      .FullName)))
        .AddCors(cors => cors.AddDefaultPolicy(policy => policy.AllowAnyHeader()
                                                               .AllowAnyMethod()
                                                               .AllowAnyOrigin()))
        .AddElsa(elsa => elsa
                         .UseEntityFrameworkPersistence(ef => ef.UseMySql(elsaConnectionString), false)
                         .AddConsoleActivities()
                         .AddJavaScriptActivities()
                         .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                         .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
                         .AddQuartzTemporalActivities()
                         .AddCommonTemporalActivities())
        .AddElsaApiEndpoints()
        .AddWorkflowContextProvider<Begehung>()

        // https://github.com/elsa-workflows/elsa-core/issues/1453
        // .AddStartupTask<RunMigrations>()
        .AddStartupTask<RunDomainMigrations>();
    }

    public void Configure(IApplicationBuilder app,
                          IWebHostEnvironment env,
                          ILogger<Startup> logger)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseCors();
      app.UseHttpActivities();
      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
