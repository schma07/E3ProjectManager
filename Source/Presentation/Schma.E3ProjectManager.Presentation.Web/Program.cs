using System;
using System.IO;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Schma.E3ProjectManager.Infrastructure.Services;
using Serilog;

using AzureKeyVaultConfigurationOptions = Azure.Extensions.AspNetCore.Configuration.Secrets.AzureKeyVaultConfigurationOptions;

namespace Schma.E3ProjectManager.Presentation.Web
{
    public class Program
    {
        private static IConfigurationRoot _configurationRoot;

        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json")
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(_configurationRoot).CreateLogger();
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    var dbInitializer = services.GetRequiredService<IDbInitializerService>();
                    logger.LogInformation($"Running database migration/seed");
                    dbInitializer.Migrate();
                    dbInitializer.Seed();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while running database migration.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var builtConfig = config.Build();

                    if (!context.HostingEnvironment.IsEnvironment("azurecontainerproduction"))
                    {
                        string keyVaultUrl = builtConfig["AzureKeyVault:Endpoint"];
                        string tenantId = builtConfig["AzureKeyVault:TenantId"];
                        string clientId = builtConfig["AzureKeyVault:ClientId"];
                        string clientSecretId = builtConfig["AzureKeyVault:ClientSecretId"];

                        var credential = new ClientSecretCredential(tenantId, clientId, clientSecretId);

                        var secretClient = new SecretClient(new Uri(keyVaultUrl), credential);
                        config.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());
                    }
                    else
                    {
                        string keyVaultUrl = builtConfig["AzureKeyVaultEndpoint"];
                        var serviceClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}