using Application.ViewModels;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WeldingPassportsApp.IntegrationTests.Controllers
{
    [Collection("Sequential")]
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public AccountControllerTests(WebApplicationFactory<WeldingPassportsApp.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/Account/Register")]
        public async Task PostAccountRegisterReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            await Utilities.MigrateAsync(_factory.Services, "it@synergrid.be");

            var client = _factory
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 209-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
