using System;

using Infrastructure;

using MassTransit;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MT.Backend.Messages.Begehungen;

namespace Web
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
      services.AddRazorPages();

      services.AddDbContext
        <DomainDbContext>(options =>
        {
          var connectionString = Configuration.GetConnectionString("Domain");
          options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

      services.AddDatabaseDeveloperPageExceptionFilter();

      services.AddMassTransit(x =>
      {
        x.SetKebabCaseEndpointNameFormatter();

        x.UsingRabbitMq((ctx, cfg) =>
        {
          var section = Configuration.GetSection("MassTransit");
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

          cfg.ConfigureEndpoints(ctx);

          var begehungen = new Uri(section.GetSection("Queues")
                                          .GetValue<string>("Begehungen"));
          EndpointConvention.Map<StarteBegehung>(begehungen);
          EndpointConvention.Map<SchließeBegehungAb>(begehungen);
          EndpointConvention.Map<VerwerfeBegehung>(begehungen);
        });
      });

      services.AddMassTransitHostedService();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage()
           .UseMigrationsEndPoint();
      }

      app.UseStaticFiles()
         .UseRouting()
         .UseAuthorization()
         .UseEndpoints(endpoints =>
         {
           endpoints.MapRazorPages();

           endpoints.MapControllerRoute("default",
                                        "{controller=Home}/{action=Index}/{id?}");
         });
    }
  }
}
