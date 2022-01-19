using System.IO;
using System.Linq;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WeldingPassportsApp.IntegrationTests.Helpers
{
    public class TestApplicationFactory<Startup> : WebApplicationFactory<Startup> where Startup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }
        protected override IHostBuilder CreateHostBuilder()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureWebHost(builder =>
                {
                    builder.UseStartup<Startup>();
                });
            return host;
        }
    }
}
