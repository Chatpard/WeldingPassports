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

                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await AddRolesAsync(roleManager);
                await AddUserWithRoleAsync(adminEmail, RolesStore.Admin, userManager);
                await AddTestUsersAsync(userManager, env);
                await AddCompanyContactIdentityUserId(context, "leen.dezillie@v-c-l.be", "tc.trainingcenter@outlook.com");
                await AddCompanyContactIdentityUserId(context, "guy.doms@vincotte.be", "ec.examcenter@outlook.com");
            }
        }

        private static async Task AddTestUsersAsync(UserManager<IdentityUser> userManager,
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

        private static async Task AddUserWithRoleAsync(string userEmail, string role, UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                user = new IdentityUser { UserName = userEmail, Email = userEmail };

                await userManager.CreateAsync(user);
                await userManager.ConfirmEmailAsync(user, await userManager.GenerateEmailConfirmationTokenAsync(user));

                await userManager.AddToRoleAsync(user, role);
            }
        }

        private static async Task AddRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in RolesStore.Roles)
            {
                var identityRole = new IdentityRole { Name = role };
                await roleManager.CreateAsync(identityRole);
                foreach (var permission in ClaimsTypesStore.Claims(role))
                {
                    await roleManager.AddClaimAsync(identityRole, new Claim(permission.Key, permission.Value));
                }
            }
        }
    
        private static async Task AddCompanyContactIdentityUserId(AppDbContext context, string companyContactEmail, string identityUserEmail)
        {
            CompanyContact companyContact = await context.CompanyContacts.Where(companyContact => companyContact.Email == companyContactEmail).FirstOrDefaultAsync();
            if (companyContact == null) return;

            IdentityUser identityUser = context.Users.Where(user => user.Email == identityUserEmail).FirstOrDefault();
            if (identityUser == null) return;

            companyContact.IdentityUserId = identityUser.Id;
            context.Update(companyContact);
            await context.SaveChangesAsync(new System.Threading.CancellationToken());
        }
    }
}
