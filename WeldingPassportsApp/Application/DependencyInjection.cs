using Application.Profiles;
using Application.Security;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Application
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton(provider => new MapperConfiguration(config =>
            {
                config.AddProfile(new AppSettingsProfile());
                config.AddProfile(new UserToApproveProfile());
                config.AddProfile(new AddressProfile(provider.GetService<IDataProtectionProvider>(),
                    provider.GetService<IDataProtectionPurposeStrings>()));
                config.AddProfile(new CompanyContactProfile(provider.GetService<IDataProtectionProvider>(),
                    provider.GetService<IDataProtectionPurposeStrings>()));
                config.AddProfile(new CompanyProfile(provider.GetService<IDataProtectionProvider>(),
                    provider.GetService<IDataProtectionPurposeStrings>()));
                config.AddProfile(new ContactProfile(provider.GetService<IDataProtectionProvider>(),
                    provider.GetService<IDataProtectionPurposeStrings>()));
                config.AddProfile(new ExaminationProfile(provider.GetService<IDataProtectionProvider>(),
                    provider.GetService<IDataProtectionPurposeStrings>()));
                config.AddProfile(new PEPassportProfile(provider.GetService<IDataProtectionProvider>(),
                    provider.GetService<IDataProtectionPurposeStrings>()));
                config.AddProfile(new PEWelderProfile(provider.GetService<IDataProtectionProvider>(),
                    provider.GetService<IDataProtectionPurposeStrings>()));
                config.AddProfile(new TrainingCenterProfile(provider.GetService<IDataProtectionProvider>(),
                    provider.GetService<IDataProtectionPurposeStrings>()));
            }).CreateMapper());

            services.AddSingleton<IDataProtectionPurposeStrings, DataProtectionPurposeStrings>();

            return services;
        }
    }
}
