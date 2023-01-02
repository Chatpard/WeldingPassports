using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WeldingPassportsApp.IntegrationTests.Helpers;

namespace WeldingPassportsApp.IntegrationTests
{
    public static class ControllerTestExtentions
    {
        public static T WithIdentity<T>(this T controller, string nameIdentifier, string name) where T : Controller
        {
            controller.EnsureHttpContext();
            var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, nameIdentifier),
                                new Claim(ClaimTypes.Name, name)
                            },"TestAuthentication"));
            controller.ControllerContext.HttpContext.User = principal;
            return controller;
        }

        public static T EnsureHttpContext<T>(this T controller) where T: Controller
        {
            if(controller.ControllerContext == null)
            {
                controller.ControllerContext = new ControllerContext();
            }

            if(controller.ControllerContext.HttpContext == null)
            {
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
            }
            return controller;
        }

    }
}
