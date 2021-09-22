using System;

using Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using MySqlConnector;

using NServiceBus;

namespace NSB.Backend
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = Host.CreateDefaultBuilder(args)
                        
                        .ConfigureLogging((context, logging) =>
                        {
                          logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                        })
                        .UseConsoleLifetime()
                        .ConfigureServices((context,services) =>
                        {
                          services.AddDbContext
                            <DomainDbContext>(options =>
                            {
                              var connectionString = context.Configuration.GetConnectionString("Domain");;
                              options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                            });

                        })
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
                                                            type.Namespace.Contains(".Commands"))
                                .DefiningEventsAs(type => type.Namespace != null &&
                                                          type.Namespace.Contains(".Events"))
                                .DefiningMessagesAs(type => type.Namespace != null &&
                                                          type.Namespace.Contains(".Messages"));

                          config.Recoverability()
                                .Immediate(s => s.NumberOfRetries(1))
                                .Delayed(s => s.NumberOfRetries(0));
                          //       .Delayed(s => s.NumberOfRetries(5).TimeIncrease(TimeSpan.FromSeconds(5)));

                          config.EnableOutbox();
                          config.EnableInstallers();

                          config.UseSerialization<NewtonsoftSerializer>();

                          var persistence = config.UsePersistence<SqlPersistence>();
                          persistence.SqlDialect<SqlDialect.MySql>();
                          
                          var connection =
                            context.Configuration.GetConnectionString("NSB");
                          Console.WriteLine($"NSB database connection: {connection}");
                          
                          persistence.ConnectionBuilder(() => new MySqlConnection(connection));

                          return config;
                        })
                        .Build();

      builder.Run();
    }
  }
}
