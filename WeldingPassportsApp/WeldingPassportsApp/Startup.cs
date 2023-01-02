using Application;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace WeldingPassportsApp
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            });

            services.RegisterInfrastructure(_config, _env);

            services.RegisterApplication();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en") { DateTimeFormat = { ShortDatePattern = "MM/dd/yyyy" }},
                    new CultureInfo("fr") { DateTimeFormat = { ShortDatePattern = "dd/MM/yyyy" }},
                    new CultureInfo("nl") { DateTimeFormat = { ShortDatePattern = "dd/MM/yyyy" }}
                };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture(supportedCultures[0]);

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;
            });

            services.AddCors();

            IMvcBuilder mvcBuilder = services.AddControllersWithViews(options => 
                {
                    var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            if (_env.IsDevelopment())
                mvcBuilder.AddRazorRuntimeCompilation();

            /*
             * Microsoft Account external login setup with ASP.NET Core
             * https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-6.0
             * The URI segment /signin-microsoft is set as the default callback of the Microsoft authentication provider.
             */

            services.AddAuthentication()
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = _config["Authentication:Microsoft:ClientId"];
                    options.ClientSecret = _config["Authentication:Microsoft:ClientSecret"];
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminCanReadEditPolicy", policy =>
                {
                    policy.RequireRole("Admin");
                    policy.RequireClaim("Admin", "CanRead", "CanReadEdit", "CanReadEditDelete");
                });
                //foreach (string claimName in ClaimsStore.Claims())
                //{
                //    foreach (string mainPermission in PermissionsStore.Permissions)
                //    {
                //        options.AddPolicy(claimName + mainPermission + "Policy", policy =>
                //            {
                //                foreach (string permission in PermissionsStore.Permissions)
                //                {
                //                    policy.RequireClaim(claimName, permission);
                //                    if (permission == mainPermission)
                //                        break;
                //                }
                //            }
                //        );
                //    }
                //}
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseCors(options => options.AllowAnyOrigin());

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=PEPassports}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "{controller}/{id?}");

            });
        }
    }
}
