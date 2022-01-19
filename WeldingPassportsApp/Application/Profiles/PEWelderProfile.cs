using Application.Security;
using Application.SQLModels;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Linq;

namespace Application.Profiles
{
    public class PEWelderProfile : Profile
    {
        private readonly IDataProtector _protector;

        public PEWelderProfile(IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);

            CreateMap<PEWelderRegistrationUIColorGroup, PEWelderIndexViewModel>()
                .ForMember(index => index.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.PEWelder.ID.ToString())))
                .ForMember(index => index.FirstName, options => options.MapFrom(group =>
                    group.PEWelder.FirstName))
                .ForMember(index => index.LastName, options => options.MapFrom(group =>
                    group.PEWelder.LastName))
                .ForMember(index => index.AVNumber, options => options.MapFrom(group =>
                    group.Registration.PEPassport.AVNumber))
                .ForMember(index => index.Color, options => options.MapFrom(group =>
                    group.UIColor.Color));

            CreateMap<PEWelder, PEWelderCreateViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.ID.ToString())));

            CreateMap<PEWelderCreateViewModel, PEWelder>()
                .ForMember(vm => vm.ID, options => options.MapFrom(group =>
                    _protector.Unprotect(group.EncryptedID)));

            CreateMap<PEWelder, PEWelderEditViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.ID.ToString())));

            CreateMap<PEWelderEditViewModel, PEWelder>()
                .ForMember(vm => vm.ID, options => options.MapFrom(group =>
                    _protector.Unprotect(group.EncryptedID)));

            CreateMap<PEWelderListRegistrationUIColorGroup, PEWelderDetailsViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.PEWelder.ID.ToString())))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(group =>
                    group.PEWelder.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(group =>
                    group.PEWelder.LastName))
                .ForMember(vm => vm.NationalNumber, options => options.MapFrom(group =>
                    group.PEWelder.NationalNumber))
                .ForMember(vm => vm.IdNumber, options => options.MapFrom(group =>
                    group.PEWelder.IdNumber))
                .ForMember(vm => vm.PEPassports, options => options.MapFrom(group =>
                    group.PEPassportRegistrationUIColorGroupList));

            CreateMap<PEPassportRegistrationUIColorGroup, PEWelderDetailsPEPassportsIndexViewModel>()
                .ForMember(index => index.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.PEPassport.ID.ToString())))
                .ForMember(index => index.AVNumber, options => options.MapFrom(group =>
                    group.PEPassport == null ? String.Empty : group.PEPassport.AVNumber))
                .ForMember(index => index.CompanyName, options => options.MapFrom(group =>
                    group.Registration == null ? String.Empty : group.Registration.Company.CompanyName))
                .ForMember(index => index.ExpiryDate, options => options.MapFrom(group =>
                    group.Registration == null ? null : group.Registration.ExpiryDate))
                .ForMember(index => index.Color, options => options.MapFrom(group =>
                    group.UIColor.Color));
        }
    }
}
