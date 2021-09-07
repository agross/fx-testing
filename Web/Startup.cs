using Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
         .UseEndpoints(endpoints => endpoints.MapRazorPages());
    }
  }
}
