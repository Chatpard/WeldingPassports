using Application;
using Application.Security;
using Domain;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace WeldingPassportsApp
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration config, IWebHostEnvironment env)
        {
            ArgumentNullException.ThrowIfNull(config, nameof(config));
            ArgumentNullException.ThrowIfNull(env, nameof(env));

            _config = config;
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

            // The Tempdata provider cookie is not essential. Make it essential
            // so Tempdata is functional when tracking is disabled.
            services.Configure<CookieTempDataProviderOptions>(options => {
                options.Cookie.IsEssential = true;
            });

            services.RegisterInfrastructure();

            services.RegisterApplication();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en") { DateTimeFormat = { ShortDatePattern = "dd/MM/yyyy" }},
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
                    options.ClientId = _config["Authentication:Microsoft:ClientId"] ?? throw new InvalidOperationException($"Microsoft Authentication CliendId is missing at '{nameof(Startup)}'.");
                    options.ClientSecret = _config["Authentication:Microsoft:ClientSecret"] ?? throw new InvalidOperationException($"Microsoft Authentication CliendSecret is missing at '{nameof(Startup)}'.");
                });

            services.AddAuthorization(options =>
            {
                foreach(string claimsType in ClaimsTypesStore.ClaimsTypes())
                {
                    foreach(KeyValuePair<string, string[]> claimsGroup in ClaimsPrincipalExtensions.ClaimsGroups())
                    {
                        options.AddPolicy(claimsType+claimsGroup.Key+"Policy", policy =>
                        {
                            policy.RequireClaim(claimsType, claimsGroup.Value);
                        });
                    }
                }
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
            if(options !=  null)
                app.UseRequestLocalization(options.Value);
            else
                app.UseRequestLocalization();

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