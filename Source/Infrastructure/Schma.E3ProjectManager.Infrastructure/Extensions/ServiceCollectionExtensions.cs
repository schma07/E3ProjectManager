using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schma.Data.Abstractions;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Infrastructure.DbContexts;
using Schma.E3ProjectManager.Infrastructure.Identity.Factories;
using Schma.E3ProjectManager.Infrastructure.Mappings;
using Schma.E3ProjectManager.Infrastructure.Models;
using Schma.E3ProjectManager.Infrastructure.Repositories;
using Schma.E3ProjectManager.Infrastructure.Services;
using Schma.EventStore.EntityFramework.Extensions;
using Schma.Messaging.Abstractions;

namespace Schma.E3ProjectManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabasePersistence(configuration);
            services.AddRepositories();
            services.AddIdentity();
            services.AddEventStoreEFCore((o) =>
            {
                o.UseInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");
                o.ConnectionStringSQL = configuration.GetConnectionString("ApplicationConnection");
            });
            services.AddScoped<OrderAddressResolver>();
            services.AddAutoMapper(config =>
            {
                config.AddExpressionMapping();
                config.AddProfile<AppProfile>();
                config.AddProfile<EventProfile>();
                config.AddProfile<UserProfile>();
            });

            services.AddScoped<IServiceBus, ServiceBusMediator>();
        }

        public static void AddSharedServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordGeneratorService, PasswordGeneratorIdentityService>();
            services.AddScoped<IApplicationConfigurationService, ApplicationConfigurationService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
        }

        private static void AddDatabasePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseInMemoryDatabase("IdentityDb"));
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));               
            }
            else
            {
                services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));                
            }
            services.AddScoped<IDbInitializerService, DbInitializerService>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<,>), typeof(DataEntityRepository<,>));
            services.AddScoped(typeof(IESRepository<,>), typeof(ESRepository<,>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();

            var passwordOptions = new PasswordOptions()
            {
                RequireDigit = false, //in accordance with ASVS 4.0
                RequiredLength = 10, //in accordance with ASVS 4.0
                RequireUppercase = false, //in accordance with ASVS 4.0
                RequireLowercase = false //in accordance with ASVS 4.0
            };

            services.AddScoped(a => passwordOptions);

            services.Configure<IdentityOptions>(options =>
            {
                options.Password = passwordOptions;

            });
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
        }        
    }
}