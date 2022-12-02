using System.Reflection;
using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schma.E3ProjectManager.Core.Application;
using Schma.E3ProjectManager.Core.Application.Extensions;
using Schma.E3ProjectManager.Infrastructure.Resources;
using Schma.E3ProjectManager.Infrastructure.Shared.Culture;
using Schma.E3ProjectManager.Presentation.Framework;
using Schma.E3ProjectManager.Presentation.Framework.Interfaces;
using Schma.E3ProjectManager.Presentation.Framework.Services;
using Schma.E3ProjectManager.Presentation.Web.Extensions;
using Schma.E3ProjectManager.Presentation.Web.Helpers;
using Schma.E3ProjectManager.Presentation.Web.Mappings;
using Schma.E3ProjectManager.Presentation.Web.Middleware;
using Schma.E3ProjectManager.Presentation.Web.Services;

namespace Schma.E3ProjectManager.Presentation.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            //ROUTING
            services.AddRouting(options => options.LowercaseUrls = true);

            //LOCALIZATION
            services.AddLocalization(options => options.ResourcesPath = "");

            //CACHING
            services.AddResponseCaching();

            //BLAZOR
            services.AddServerSideBlazor().AddCircuitOptions(options =>
            {
                //if (HostEnvironment.IsDevelopment())
                options.DetailedErrors = true;
            });

            services.AddBlazoredToast();
            services.AddBlazoredModal();
            services.AddScoped<IModalService, ModalService>();
            services.AddScoped<IToastService, ToastService>();
            services.AddScoped<IAuthorizationStateProvider, AuthorizationStateProvider>();

            //MVC
            var mvcBuilder = services.AddMvc(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            mvcBuilder.AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix);
            mvcBuilder.AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedResources).GetTypeInfo().Assembly.FullName);
                    return factory.Create("SharedResources", assemblyName.Name);
                };
            });
#if (DEBUG)
            mvcBuilder.AddRazorRuntimeCompilation();
#endif


            services.AddInfrastructure(Configuration);

            services.AddScoped(typeof(RolesToMultiSelectResolver<>));
            services.AddScoped<ILocalizationKeyProvider, LocalizationKeyProvider>();
            services.AddScoped<UserHelper>();
            services.AddApplicationLayer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            var basePath = configuration.GetValue<string>("BasePath");
            if (!string.IsNullOrWhiteSpace(basePath))
                app.UsePathBase(basePath);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMustChangePassword();

            var supportedCultures = new[] { "de", "en" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            localizationOptions.RequestCultureProviders.Insert(0, new UserProfileRequestCultureProvider());
            app.UseRequestLocalization(localizationOptions);

            app.UseResponseCaching();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapBlazorHub();
            });


        }
    }
}