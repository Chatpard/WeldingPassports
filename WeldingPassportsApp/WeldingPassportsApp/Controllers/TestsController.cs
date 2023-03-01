using Application.Security;
using Domain;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeldingPassportsApp.Controllers
{
    [Authorize]
    public class TestsController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDataProtector _protector;

        public TestsController(IWebHostEnvironment env, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _env = env;
            _signInManager = signInManager;
            _userManager = userManager;
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);
        }

        public async Task Authentication(string role)
        {
            if (_env.IsDevelopment())
            {
                IdentityUser user = null;

                if (role == RolesStore.Admin)
                    user = await _userManager.FindByEmailAsync("it.pepassportsapp@outlook.com");
                else if (role == RolesStore.TC)
                    user = await _userManager.FindByEmailAsync("tc.trainingcenter@outlook.com");
                else if (role == RolesStore.DSO)
                    user = await _userManager.FindByEmailAsync("dgo.distributiongridoperator@outlook.com");
                else if (role == RolesStore.EC)
                    user = await _userManager.FindByEmailAsync("ec.examcenter@outlook.com");

                if (user != null)
                    await _signInManager.SignInAsync(user, isPersistent: false);
            }
            else
                RedirectToAction("Index", "PEPassports");
        }

        public string EncryptedID()
        {
            return _protector.Protect("1");
        }
    }
}
