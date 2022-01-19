using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WeldingPassportsApp.IntegrationTests.Helpers
{
    public static class WebApplicationFactoryExtensions
    {
        public static WebApplicationFactory<T> WithAuthentication<T>(this WebApplicationFactory<T> factory, TestClaimsProvider claimsProvider) where T : class
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<TestClaimsProvider>(_ => claimsProvider);

                    services.AddAuthentication("Identity.TestApplication")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Identity.TestApplication", options => { });

                    services.AddAuthorization(options =>
                    {
                        var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder("Identity.TestApplication").RequireAuthenticatedUser();
                        options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

                        foreach (string roleName in RolesStore.Roles)
                        {
                            options.AddPolicy(roleName + "RolePolicy", policy =>
                            {
                                policy.RequireRole(roleName);
                                policy.AddAuthenticationSchemes("Identity.TestApplication");
                            });
                        }
                    });
                    services.AddAntiforgery(t =>
                    {
                        t.Cookie.Name = AntiForgeryTokenExtractor.AntiForgeryCookieName;
                        t.FormFieldName = AntiForgeryTokenExtractor.AntiForgeryTokeName;
                    });
                });
            });
        }

        public static HttpClient CreateClientWithTestAuth<T> (this WebApplicationFactory<T> factory, TestClaimsProvider claimsProvider) where T : class
        {
            var client = factory.WithAuthentication(claimsProvider).CreateClient( new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            return client;
        }
    }
}
