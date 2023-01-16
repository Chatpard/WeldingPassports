using Application.Security;
using Application.SQLModels;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Linq;

namespace Application.Profiles
{
    public class CompanyProfile : Profile
    {
        private readonly IDataProtector _protector;

        public CompanyProfile(IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);

            CreateMap<Company, CompanyIndexViewModel>()
                .ForMember(index => index.EncryptedID, options => options.MapFrom(company =>
                    _protector.Protect(company.ID.ToString())))
                .ForMember(index => index.CompanyName, options => options.MapFrom(company =>
                    company.CompanyName))
                .ForMember(index => index.CompanyMainPhone, options => options.MapFrom(company =>
                    company.CompanyMainPhone))
                .ForMember(index => index.CompanyEmail, options => options.MapFrom(company =>
                    company.CompanyEmail))
                .ForMember(index => index.WebPage, options => options.MapFrom(company =>
                    company.WebPage));

            CreateMap<CompanyCreateViewModel, Company>();

            CreateMap<Company, CompanyDetailsViewModel>()
                .ForMember(index => index.EncryptedID, options => options.MapFrom(company =>
                    _protector.Protect(company.ID.ToString())))
                .ForMember(index => index.BusinessAddress, options => options.MapFrom(company =>
                    company.Address.BusinessAddress))
                .ForMember(index => index.BusinessAddressCity, options => options.MapFrom(company =>
                    company.Address.BusinessAddressCity))
                .ForMember(index => index.BusinessAddressPostalCode, options => options.MapFrom(company =>
                    company.Address.BusinessAddressPostalCode))
                .ForMember(index => index.BusinessAddressCountry, options => options.MapFrom(company =>
                    company.Address.BusinessAddressCountry));

            CreateMap<Company, CompanyEditViewModel>()
                .ForMember(index => index.EncryptedID, options => options.MapFrom(company =>
                    _protector.Protect(company.ID.ToString())));

            CreateMap<CompanyEditViewModel, Company>()
                .ForMember(index => index.ID, options => options.MapFrom(company =>
                    _protector.Unprotect(company.EncryptedID)));
        }
    }
}
