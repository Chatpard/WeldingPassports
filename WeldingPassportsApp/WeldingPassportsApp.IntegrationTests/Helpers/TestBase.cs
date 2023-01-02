using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace WeldingPassportsApp.IntegrationTests.Helpers
{

    public abstract class TestBase : IClassFixture<TestApplicationFactory<Startup>>
    {
        public WebApplicationFactory<Startup> Factory { get; }

        public TestBase(TestApplicationFactory<Startup> factory)
        {
            Factory = factory;
        }
    }
}
