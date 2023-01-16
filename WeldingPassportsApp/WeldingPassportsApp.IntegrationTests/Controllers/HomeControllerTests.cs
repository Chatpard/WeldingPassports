using Application.Security;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WeldingPassportsApp.Controllers;
using WeldingPassportsApp.IntegrationTests.Helpers;
using Xunit;

namespace WeldingPassportsApp.IntegrationTests.Controllers
{
    public class HomeControllerTests : TestBase
    {
        
        public HomeControllerTests(TestApplicationFactory<Startup> factory) : base(factory)
        {
            
        }

        [Theory]
        [InlineData("/Home")]
        [InlineData("/Home/Index")]
        [InlineData("/Home/Error")]
        public async Task Get_EndpointRedirectsAndCorrectContentTypeForUnauthenticatedUser(string url)
        {
            // Arrange
            await Utilities.MigrateAsync(Factory.Services, "it@synergrid.be");
            var client = Factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode); // Status Code 302
            Assert.Contains("/Account/Login", response.Headers.Location.OriginalString);
        }

        [Theory]
        [InlineData("/Home/SetLanguage")]
        public async Task Post_EndpointRedirectsAnUnauthenticatedUser(string url)
        {
            // Arrange
            await Utilities.MigrateAsync(Factory.Services, "it@synergrid.be");
            var client = Factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
            var content = new StringContent("/");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode); // Status Code 302
        }

        [Theory]
        [InlineData("/Home/Privacy")]
        public async Task Get_EndpointReturnSuccessAndCorrectContentTypeForUnAuthenticatedUser(string url)
        {
            // Arrange
            await Utilities.MigrateAsync(Factory.Services, "it@synergrid.be");
            var client = Factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            await client.AuthenticateTestUser(RolesStore.Admin);

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }


        [Theory]
        [InlineData("/Home")]
        [InlineData("/Home/Index")]
        [InlineData("/Home/Error")]
        public async Task Get_EndpointReturnSuccessAndCorrectContentTypeForAuthenticatedUser(string url)
        {
            // Arrange
            await Utilities.MigrateAsync(Factory.Services, "it@synergrid.be");
            var client = Factory.CreateClientWithTestAuth(TestClaimsProvider.WithUserClaims());

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 209-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
