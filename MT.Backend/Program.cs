using System;

using Infrastructure;

using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MT.Backend.Begehungen;
using MT.Backend.Emails.Commands;

namespace MT.Backend
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder =
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging((context, logging) =>
            {
              var root = (IConfigurationRoot) context.Configuration;
              Console.WriteLine(root.GetDebugView());

              logging.AddConfiguration(context.Configuration.GetSection("Logging"));
            })
            .UseConsoleLifetime()
            .ConfigureServices((context, services) =>
            {
              services.AddDbContext
                <DomainDbContext>(options =>
                {
                  var connectionString =
                    context.Configuration.GetConnectionString("Domain");

                  options.UseMySql(connectionString,
                                   ServerVersion.AutoDetect(connectionString));
                });

              services.AddMassTransit(x =>
              {
                var connectionString = context.Configuration.GetConnectionString("Marten");

                // x.AddSagaRepository<BegehungSaga>()
                //  .MartenRepository(connectionString,
                //                    o =>
                //                    {
                //                      o.Schema.For<BegehungSaga>()
                //                       .UseOptimisticConcurrency(true)
                //                       .Index(i => i.BegehungId,
                //                              c => c.IsUnique = true);
                //
                //                      o.CreateDatabasesForTenants(c =>
                //                      {
                //                        c.ForTenant()
                //                         .CheckAgainstPgDatabase()
                //                         .WithOwner("postgres")
                //                         .WithEncoding("UTF-8")
                //                         .ConnectionLimit(-1);
                //                      });
                //                    });

                x.AddSagaStateMachine<BegehungSagaAsStateMachine, Begehungen.Begehungen>()
                 .MartenRepository(connectionString,
                                   o =>
                                   {
                                     o.Schema.For<Begehungen.Begehungen>()
                                      .UseOptimisticConcurrency(true)
                                      .Identity(i => i.CorrelationId)
                                      .Index(i => i.BegehungId,
                                             c => c.IsUnique = true);

                                     o.CreateDatabasesForTenants(c =>
                                     {
                                       c.ForTenant()
                                        .CheckAgainstPgDatabase()
                                        .WithOwner("postgres")
                                        .WithEncoding("UTF-8")
                                        .ConnectionLimit(-1);
                                     });
                                   });

                x.AddConsumersFromNamespaceContaining<Program>();

                x.AddSagaStateMachinesFromNamespaceContaining<Program>();
                x.AddSagasFromNamespaceContaining<Program>();

                x.AddDelayedMessageScheduler();

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                  var section = context.Configuration.GetSection("MassTransit");
                  var url = section.GetValue<string>("Url");
                  var host = section.GetValue<string>("Host");
                  var userName = section.GetValue<string>("UserName");
                  var password = section.GetValue<string>("Password");
                  if (section == null || url == null || host == null)
                  {
                    throw new
                      ConfigurationException("Section 'mass-transit' configuration settings are not found in appSettings.json");
                  }

                  cfg.Host($"rabbitmq://{url}/{host}",
                           configurator =>
                           {
                             configurator.Username(userName);
                             configurator.Password(password);
                           });

                  cfg.UseDelayedMessageScheduler();

                  var emails = new Uri(section.GetSection("Queues")
                                              .GetValue<string>("E-Mails"));
                  EndpointConvention.Map<SendeEmail>(emails);

                  cfg.ConfigureEndpoints(ctx);
                });
              });

              services.AddHostedService<Worker>();
            })
            .Build();

      builder.Run();
    }
  }
}
