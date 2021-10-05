using System;

using Domain.Models;

using Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NSB.Backend.Begehungen.Commands;

using NServiceBus;

namespace Web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args)
                 .UseNServiceBus(context =>
                 {
                   var root = (IConfigurationRoot) context.Configuration;
                   Console.WriteLine(root.GetDebugView());

                   var config = new EndpointConfiguration("fx");
                   var transport = config.UseTransport<RabbitMQTransport>();
                   transport.ConnectionString(context.Configuration.GetConnectionString("RabbitMQ"));
                   transport.UseConventionalRoutingTopology();

                   config.Conventions()
                         .DefiningCommandsAs(type => type.Namespace != null &&
                                                     (type.Namespace.Contains(".Commands") ||
                                                      type.Namespace.StartsWith("Domain.Models")));

                   config.UseSerialization<NewtonsoftSerializer>();

                   transport.Routing()
                            .RouteToEndpoint(typeof(StarteBegehung).Assembly,
                                             "fx");
                   transport.Routing()
                            .RouteToEndpoint(typeof(BegehungAbschließen).Assembly,
                                             "fx");

                   config.SendOnly();

                   return config;
                 })
                 .Build();

      CreateDbIfNotExists(host);

      host.Run();
    }

    static void CreateDbIfNotExists(IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          var context = services.GetRequiredService<DomainDbContext>();
          context.Database.EnsureCreated();
          context.Database.Migrate();
          Stammdaten.Initialize(context);
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occurred creating the DB");
        }
      }
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
  }
}