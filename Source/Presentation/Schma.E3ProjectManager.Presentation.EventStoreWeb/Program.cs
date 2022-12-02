using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Infrastructure.DbContexts;
using Schma.E3ProjectManager.Presentation.EventStoreWeb.Services;

namespace Company.WebApplication1
{
    public class Program
    {
        private static IConfigurationRoot _configurationRoot;

        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = WebApplication.CreateBuilder(args);
            _configurationRoot = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile($"appsettings.{environment}.json", true)
                                .AddEnvironmentVariables()
                                .Build();

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMudServices();
            builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            builder.Services.AddScoped<EventStoreService>();
            builder.Services.AddDbContext<EventStoreDbContext>(options => options.UseSqlServer(_configurationRoot.GetConnectionString("ApplicationConnection")));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}