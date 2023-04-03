using Application.Security;
using Domain;
using Domain.Models;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WeldingPassportsApp.Controllers;

namespace WeldingPassportsApp
{
    public static class Utilities
    {
        public static IActionResult ErrorView(IWebHostEnvironment env, Controller controller, Exception e, string errorTitle)
        {
            if (env.IsDevelopment())
            {
                controller.ViewBag.ErrorTitle = errorTitle;
                controller.ViewBag.ErrorMessage = e.Message;

                return controller.View(nameof(ErrorController.Error));
            }

            throw e;
        }

        public static string GetNameOfController(this Type controllerType)
        {
            if (controllerType.IsSubclassOf(typeof(Controller)))
                return controllerType.Name.Replace(nameof(Controller), string.Empty);

            throw new InvalidOperationException("Input is not a subclass of 'Controller'.");
        }

        public static async Task<IHost> MigrateAsync(this IHost host, string adminEmail)
        {
            await MigrateAsync(host.Services, adminEmail);

            return host;
        }

        public static async Task MigrateAsync(IServiceProvider services, string adminEmail)
        {
            using (var scope = services.CreateScope())
            {
                IWebHostEnvironment env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (env.IsDevelopment() || env.IsStaging())
                    await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();

                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                RoleManager<AppRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

                await AddRolesAsync(roleManager);
                await AddUserWithRoleAsync(adminEmail, RolesStore.Admin, userManager);
                await AddTestUsersAsync(userManager, env);
                await AddCompanyContactAppUserId(context, "christian.moenaert@synergrid.be", "it.pepassportsapp@outlook.com");
                await AddCompanyContactAppUserId(context, "leen.dezillie@v-c-l.be", "tc.trainingcenter@outlook.com");
                await AddCompanyContactAppUserId(context, "guy.doms@vincotte.be", "ec.examcenter@outlook.com");
                await AddCompanyContactAppUserId(context, "davy.gijsels@fluvius.be", "dgo.distributiongridoperator@outlook.com");
            }
        }

        private static async Task AddTestUsersAsync(UserManager<AppUser> userManager,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsStaging())
            {
                // Admin
                await AddUserWithRoleAsync("it.pepassportsapp@outlook.com", RolesStore.Admin, userManager);

                // TC
                await AddUserWithRoleAsync("tc.trainingcenter@outlook.com", RolesStore.TC, userManager);

                // EC
                await AddUserWithRoleAsync("ec.examcenter@outlook.com", RolesStore.EC, userManager);

                // DGO
                await AddUserWithRoleAsync("dgo.distributiongridoperator@outlook.com", RolesStore.DSO, userManager);
            }
        }

        private static async Task AddUserWithRoleAsync(string userEmail, string role, UserManager<AppUser> userManager)
        {
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                user = new AppUser { UserName = userEmail, Email = userEmail };

                await userManager.CreateAsync(user);
                await userManager.ConfirmEmailAsync(user, await userManager.GenerateEmailConfirmationTokenAsync(user));

                await userManager.AddToRoleAsync(user, role);
            }
        }

        private static async Task AddRolesAsync(RoleManager<AppRole> roleManager)
        {
            foreach (var role in RolesStore.Roles)
            {
                var AppRole = new AppRole { Name = role, RoleName = RolesStore.ViewRoles.GetValueOrDefault(role) };
                await roleManager.CreateAsync(AppRole);
                foreach (var permission in ClaimsTypesStore.Claims(role))
                {
                    await roleManager.AddClaimAsync(AppRole, new Claim(permission.Key, permission.Value));
                }
            }
        }
    
        private static async Task AddCompanyContactAppUserId(AppDbContext context, string companyContactEmail, string AppUserEmail)
        {
            CompanyContact companyContact = await context.CompanyContacts.Where(companyContact => companyContact.Email == companyContactEmail).FirstOrDefaultAsync();
            if (companyContact == null) return;

            AppUser AppUser = context.Users.Where(user => user.Email == AppUserEmail).FirstOrDefault();
            if (AppUser == null) return;

            companyContact.AppUserId = AppUser.Id;
            context.Update(companyContact);
            await context.SaveChangesAsync(new System.Threading.CancellationToken());
        }
    }
}
