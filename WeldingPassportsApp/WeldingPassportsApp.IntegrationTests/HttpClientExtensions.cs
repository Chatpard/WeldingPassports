using Application.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeldingPassportsApp.IntegrationTests
{
    public static class HttpClientExtensions
    {
        public static async Task AuthenticateTestUser(this HttpClient client, string role)
        {
            if (RolesStore.Roles.Contains(role))
                await client.GetAsync($"Tests/Authentication?role={role}");
            else
                throw new InvalidOperationException("Invalid role.");
        }
    }
}
