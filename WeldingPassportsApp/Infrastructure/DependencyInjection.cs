﻿using Application.Interfaces;
using Application.Interfaces.Repositories.API;
using Application.Interfaces.Repositories.SQL;
using Domain.Models;
using Infrastructure.Repositories.API;
using Infrastructure.Repositories.SQL;
using Infrastructure.Services;
using Infrastructure.Services.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            services.AddScoped(provider => provider.GetService<IAppDbContext>());

            services.AddIdentity<AppUser, AppRole>(options =>
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
            services.AddScoped<IUsersSQLRepository, UsersSQLRepository>();
            services.AddScoped<IUserToApproveRepository, UserToApproveSQLRepository>();
            services.AddScoped<IAppRolesSQLRepository, AppRolesSQLRepository>();
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
            services.AddScoped<ICertificatesSQLRepository, CertificatesSQLRepository>();
            services.AddScoped<IRevokeSQLRepository, RevokeSQLRepository>();
        }

        private static void AddAPIRepositoriesToServices(this IServiceCollection services)
        {
            services.AddScoped<ITrainingCentersAPIRepository, TrainingCentersAPIRepository>();
            services.AddScoped<IPEPassportsAPIRepository, PEPassportsAPIRepository>();
            services.AddScoped<ICompanyContactsAPIRepository, CompanyContactsAPIRepository>();
            services.AddScoped<ICertificationsAPIRepository, CertificatesAPIRepository>();
            services.AddScoped<IPEWeldersAPIRepository, PEWeldersAPIRepository>();
            services.AddScoped<IExamCentersAPIRepository, ExamCentersAPIRepository>();
        }
    }
}
