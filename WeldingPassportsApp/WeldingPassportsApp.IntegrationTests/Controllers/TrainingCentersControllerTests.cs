using Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WeldingPassportsApp.IntegrationTests.Controllers
{
    [Collection("Sequential")]
    public class TrainingCentersControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public TrainingCentersControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/TrainingCenters/Index")]
        [InlineData("/TrainingCenters/Details", true)]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url, bool hasRouteID = false)
        {
            // Arrange
            await Utilities.MigrateAsync(_factory.Services, "it@synergrid.be");

            var client = _factory
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            await client.AuthenticateTestUser(RolesStore.Admin);

            // Act
            HttpResponseMessage response;

            if (hasRouteID)
            {
                string id = await TestUtilities.GetEncryptedIDAsync(client);
                response = await client.GetAsync($"{url}/{id}");
            }
            else
                response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 209-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
