using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WeldingPassportsApp.IntegrationTests.Helpers
{
    public class TestClaimsProvider
    {
        public IList<Claim> Claims { get; }

        public TestClaimsProvider(IList<Claim> claims)
        {
            Claims = claims;
        }

        public TestClaimsProvider()
        {
            Claims = new List<Claim>();
        }

        public static TestClaimsProvider WithAdminClaims()
        {
            var provider = new TestClaimsProvider();
            provider.Claims.Add(new Claim(ClaimTypes.Name, RolesStore.Admin));
            provider.Claims.Add(new Claim(ClaimTypes.Role, RolesStore.Admin));
            return provider;
        }

        public static TestClaimsProvider WithTCClaims()
        {
            var provider = new TestClaimsProvider();
            provider.Claims.Add(new Claim(ClaimTypes.Role, RolesStore.TC));
            return provider;
        }
        public static TestClaimsProvider WithDSOClaims()
        {
            var provider = new TestClaimsProvider();
            provider.Claims.Add(new Claim(ClaimTypes.Role, RolesStore.DSO));
            return provider;
        }
        public static TestClaimsProvider WithECClaims()
        {
            var provider = new TestClaimsProvider();
            provider.Claims.Add(new Claim(ClaimTypes.Role, RolesStore.EC));
            return provider;
        }

        public static TestClaimsProvider WithUserClaims()
        {
            var provider = new TestClaimsProvider();
            provider.Claims.Add(new Claim(ClaimTypes.Name, "User"));
            return provider;
        }
    }
}
