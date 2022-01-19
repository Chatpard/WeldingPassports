using Application.Interfaces;
using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Infrastructure.Repositories.API;
using Infrastructure.Repositories.SQL;
using Infrastructure.Services;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
        {
            services.AddDbContext<AppDbContext>(
                options =>
                {
                    options.UseSqlServer(config.GetConnectionString("WeldingPassportsDBConnection"));
                    if (env.IsDevelopment())
                        options.EnableSensitiveDataLogging();
                }
            );

            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<IMailService, SendGridMailService>();

            services.AddSQLRepositoriesToServices();

            services.AddAPIRepositoriesToServices();

            return services;
        }

        private static void AddSQLRepositoriesToServices(this IServiceCollection services)
        {
            services.AddScoped<IAppSettingsSQLRepository, AppSettingsSQLRepository>();
            services.AddScoped<IUserToApproveRepository, UserToApproveSQLRepository>();
            services.AddScoped<IPEPassportsSQLRepository, PEPassportsSQLRepository>();
            services.AddScoped<IPEPassportsSQLRepository, PEPassportsSQLRepository>();
            services.AddScoped<IPEWeldersSQLRepository, PEWeldersSQLRepository>();
            services.AddScoped<IExaminationsSQLRepository, ExaminationsSQLRepository>();
            services.AddScoped<ITrainingCentersSQLRepository, TrainingCentersSQLRepository>();
            services.AddScoped<ICompaniesSQLRepository, CompaniesSQLRepository>();
            services.AddScoped<IContactsSQLRepository, ContactsSQLRepository>();
            services.AddScoped<ICompanyContactsSQLRepository, CompanyContactsSQLRepository>();
            services.AddScoped<IAddressesSQLRepository, AddressesSQLRepository>();
            services.AddScoped<IExamCentersSQLRepository, ExamCentersSQLRepository>();
            services.AddScoped<IListTrainingCentersSQLRepository, ListTrainingCentersSQLRepository>();
        }

        private static void AddAPIRepositoriesToServices(this IServiceCollection services)
        {
            services.AddScoped<ITrainingCentersAPIRepository, TrainingCentersAPIRepository>();
            services.AddScoped<IPEPassportsAPIRepository, PEPassportsAPIRepository>();
            services.AddScoped<ICompanyContactsAPIRepository, CompanyContactsAPIRepository>();
        }
    }
}
