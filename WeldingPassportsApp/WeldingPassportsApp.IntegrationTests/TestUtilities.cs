using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeldingPassportsApp.IntegrationTests
{
    public static class TestUtilities
    {
        public static async Task<string> GetEncryptedIDAsync(HttpClient client)
        {
            return await (await client.GetAsync($"/Tests/EncryptedID"))
                .Content.ReadAsStringAsync();
        }
    }
}
