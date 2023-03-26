using Application.Security;
using Application.SQLModels;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.Internal;
using Domain.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Linq;

namespace Application.Profiles
{
    public class CompanyContactProfile : Profile
    {
        private readonly IDataProtector _protector;

        public CompanyContactProfile(IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);

            CreateMap<CompanyContact, CompanyContactDetailsViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(Convert.ToString(group.ID))))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(group =>
                    group.Contact.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(group =>
                    group.Contact.LastName))
                .ForMember(vm => vm.CompanyName, options => options.MapFrom(group =>
                    group.Company.CompanyName))
                .ForMember(vm => vm.Birthday, options => options.MapFrom(group =>
                    group.Contact.Birthday))
                .ForMember(vm => vm.Email, options => options.MapFrom(group =>
                    group.Email))
                .ForMember(vm => vm.JobTitle, options => options.MapFrom(group =>
                    group.JobTitle))
                .ForMember(vm => vm.BusinessPhone, options => options.MapFrom(group =>
                    group.BusinessPhone))
                .ForMember(vm => vm.MobilePhone, options => options.MapFrom(group =>
                    group.MobilePhone))
                .ForMember(vm => vm.BusinessAddress, options => options.MapFrom(group =>
                    group.Company.Address.BusinessAddress))
                .ForMember(vm => vm.BusinessAddressPostalCode, options => options.MapFrom(group =>
                    group.Company.Address.BusinessAddressPostalCode))
                .ForMember(vm => vm.BusinessAddressCity, options => options.MapFrom(group =>
                    group.Company.Address.BusinessAddressCity))
                .ForMember(vm => vm.BusinessAddressCountry, options => options.MapFrom(group =>
                    group.Company.Address.BusinessAddressCountry));

            CreateMap<CompanyContact, CompanyContactCreateViewModel>();

            CreateMap<CompanyContactCreateViewModel, CompanyContact>();

            CreateMap<CompanyContact, CompanyContactEditViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(Convert.ToString(group.ID))))
                .ForMember(vm => vm.AppUserID, options => options.MapFrom(CompanyContact =>
                    CompanyContact.AppUserId))
                .ForMember(vm => vm.AppRoleID, options => options.MapFrom(companyContact =>
                    companyContact.AppUser.AppUserRoles.FirstOrDefault().RoleId));

            CreateMap<CompanyContactEditViewModel, CompanyContact>()
                .ForMember(vm => vm.ID, options => options.MapFrom(group =>
                    _protector.Unprotect(group.EncryptedID)))
                .ForMember(companyContact => companyContact.AppUserId, options => options.MapFrom(vm =>
                    vm.AppUserID));

            CreateMap<CompanyContact, CompanyContactIndexViewModel>()
                .ForMember(index => index.EncryptedID, options => options.MapFrom(companyContact =>
                    _protector.Protect(Convert.ToString(companyContact.ID))))
                .ForMember(index => index.FirstName, options => options.MapFrom(companyContact =>
                    companyContact.Contact.FirstName))
                .ForMember(index => index.LastName, options => options.MapFrom(companyContact =>
                    companyContact.Contact.LastName))
                .ForMember(index => index.Email, options => options.MapFrom(companyContact =>
                    companyContact.Email))
                .ForMember(index => index.BusinessPhone, options => options.MapFrom(companyContact =>
                        companyContact.BusinessPhone))
                .ForMember(index => index.MobilePhone, options => options.MapFrom(companyContact =>
                        companyContact.MobilePhone))
                .ForMember(index => index.RoleName, options => options.MapFrom(companyContact =>
                    companyContact.AppUser.AppUserRoles.FirstOrDefault().AppRole.RoleName));
        }
    }
}
