using Application.ViewModels;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeldingPassportsApp.IntegrationTests.Helpers;
using Xunit;

namespace WeldingPassportsApp.IntegrationTests.Controllers
{
    public class AdminControllerTests : TestBase
    {
        public AdminControllerTests(TestApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Theory]
        [InlineData("/Admin/AppSettingsDetails")]
        [InlineData("/Admin/AppSettingsEdit/1")]
        [InlineData("/Admin/UsersToApproveIndex")]
        [InlineData("/Admin/UserToApproveDetails/1")]
        [InlineData("/Admin/DeleteUser/1")]
        public async Task Get_EndpointReturnSuccessAndCorrectContentTypeForAdminUser(string url)
        {
            // Arrange
            await Utilities.MigrateAsync(Factory.Services, "it@synergrid.be");
            var client = Factory.CreateClientWithTestAuth(TestClaimsProvider.WithAdminClaims());

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 209-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Admin/AppSettingsEdit")]
        public async Task Post_AppSettingsRedirectsAndCorrectLocationForAdminUser(string url)
        {
            // Arrange
            await Utilities.MigrateAsync(Factory.Services, "it@synergrid.be");
            var client = Factory.CreateClientWithTestAuth(TestClaimsProvider.WithAdminClaims());


            // Act
            var initialresponse = await client.GetAsync(url);
            var antiForgeryValues = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initialresponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, url);
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(
                new StringSegment(AntiForgeryTokenExtractor.AntiForgeryCookieName),
                new StringSegment(antiForgeryValues.cookieValue)
            ).ToString());

            var formModel = new Dictionary<string, string>
            {
                { AntiForgeryTokenExtractor.AntiForgeryTokeName, antiForgeryValues.tokenValue },
                { "ID", "1" },
                { "MaxExpiryDays", "365" },
                { "MaxExtensionDays", "270" },
                { "MaxInAdvanceDays", "30" }
            };

            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await client.SendAsync(postRequest);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode); // Status Code 302
            Assert.Equal("/Admin/AppSettingsDetails", response.Headers.Location.ToString());
        }

        [Theory]
        [InlineData("/Admin/ApproveUser/1")]
        public async Task Post_ApproveUserReturnSuccessAndCorrectContentTypeForAdminUser(string url)
        {
            // Arrange
            await Utilities.MigrateAsync(Factory.Services, "it@synergrid.be");
            var client = Factory.CreateClientWithTestAuth(TestClaimsProvider.WithAdminClaims());
            var vm = new Dictionary<string, string>
            {
                {"id", "1"},
                {"role", "Admin" }
            };
            var jsonString = JsonConvert.SerializeObject(vm);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 209-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
