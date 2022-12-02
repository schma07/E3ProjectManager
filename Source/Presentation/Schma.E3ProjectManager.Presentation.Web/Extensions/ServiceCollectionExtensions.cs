using System;
using AutoMapper.Extensions.ExpressionMapping;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Infrastructure.Extensions;
using Schma.E3ProjectManager.Infrastructure.Resources;
using Schma.E3ProjectManager.Infrastructure.Services;
using Schma.E3ProjectManager.Presentation.Framework.Services;
using Schma.E3ProjectManager.Presentation.Web.Mappings;
using Schma.E3ProjectManager.Presentation.Web.Services;

namespace Schma.E3ProjectManager.Presentation.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddInfrastructureLayer(configuration);
            services.AddSharedServices();
            services.AddApplicationCookie();

            services.AddScoped<LocalizedRolesResolver>();
            services.AddScoped<LoaderService>();
            services.AddAutoMapper(config =>
            {
                config.AddProfile<AppProfile>();
                config.AddExpressionMapping();
            });
            
            services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddStorageOptions(configuration);
            services.AddValidatorsFromAssemblyContaining<Startup>();
        }

        private static void AddApplicationCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Shared/AccessDenied";
                options.Cookie.Name = "Schma.E3ProjectManager.AUTH";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/auth/login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });
        }

        private static void AddStorageOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileStorageOptions>(x => configuration.GetSection("Storage").Bind(x));
        }
    }
}