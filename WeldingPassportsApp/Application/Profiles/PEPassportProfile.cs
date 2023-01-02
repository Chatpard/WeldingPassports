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
    public class PEPassportProfile : Profile
    {
        private readonly IDataProtector _protector;

        public PEPassportProfile(IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);

            CreateMap<PEPassportRegistrationUIColorGroup, PEPassportIndexViewModel>()
                .ForMember(index => index.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.PEPassport.ID.ToString())))
                .ForMember(index => index.Letter, options => options.MapFrom(group =>
                    group.PEPassport.TrainingCenter.Letter))
                .ForMember(index => index.AVNumber, options => options.MapFrom(group =>
                    group.PEPassport.AVNumber))
                .ForMember(index => index.FirstName, options => options.MapFrom(group =>
                    group.PEPassport.PEWelder.FirstName))
                .ForMember(index => index.LastName, options => options.MapFrom(group =>
                    group.PEPassport.PEWelder.LastName))
                .ForMember(index => index.CompanyName, options => options.MapFrom(group =>
                    group.Registration == null ? String.Empty : group.Registration.Company.CompanyName))
                .ForMember(index => index.ExpiryDate, options => options.MapFrom(group =>
                    group.Registration.ExpiryDate))
                .ForMember(index => index.Color, options => options.MapFrom(group =>
                    group.UIColor.Color))
            .ReverseMap();

            CreateMap<PEPassportPEWelderRegistrationUIColorsGroup, PEPassportDetailsViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.PEPassport.ID.ToString())))
                .ForMember(vm => vm.Letter, options => options.MapFrom(group =>
                    group.PEPassport.TrainingCenter.Letter))
                .ForMember(vm => vm.AVNumber, options => options.MapFrom(group =>
                    group.PEPassport.AVNumber))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(group =>
                    group.PEWelder.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(group =>
                    group.PEWelder.LastName))
                .ForMember(vm => vm.NationalNumber, options => options.MapFrom(group =>
                    group.PEWelder.NationalNumber))
                .ForMember(vm => vm.IdNumber, options => options.MapFrom(group =>
                    group.PEWelder.IdNumber))
                .ForMember(vm => vm.Certifications, options => options.MapFrom(group =>
                    group.RegistrationUIColors));

            CreateMap<PEPassportPEWelderRegistrationUIColorsGroup, PEPassportUpdateViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.PEPassport.ID.ToString())))
                .ForMember(vm => vm.Letter, options => options.MapFrom(group =>
                    group.PEPassport.TrainingCenter.Letter))
                .ForMember(vm => vm.AVNumber, options => options.MapFrom(group =>
                    group.PEPassport.AVNumber))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(group =>
                    group.PEWelder.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(group =>
                    group.PEWelder.LastName))
                .ForMember(vm => vm.NationalNumber, options => options.MapFrom(group =>
                    group.PEWelder.NationalNumber))
                .ForMember(vm => vm.IdNumber, options => options.MapFrom(group =>
                    group.PEWelder.IdNumber))
                .ForMember(vm => vm.Certifications, options => options.MapFrom(group =>
                    group.RegistrationUIColors));

            CreateMap<PEPassportDetailsRegistrationsIndexViewModel, Registration>()
                .ForMember(registration => registration.ID, options => options.MapFrom(vm =>
                    _protector.Unprotect(vm.EncryptedID)));
            
            CreateMap<PEPassport, PEPassportCreateViewModel>()
                .ForMember(vm => vm.Letter, options => options.MapFrom(passport =>
                    passport.TrainingCenter.Letter))
                .ForMember(vm => vm.TrainingCenterID, options => options.MapFrom(passport =>
                    passport.TrainingCenterID))
                .ForMember(vm => vm.PEWelderID, options => options.MapFrom(passport =>
                    passport.PEWelderID));

            CreateMap<PEPassportCreateViewModel, PEPassport>()
                .ForMember(pePassport => pePassport.AVNumber, options => options.MapFrom(vm =>
                    vm.AVNumber))
                .ForMember(pePassport => pePassport.TrainingCenterID, options => options.MapFrom(vm =>
                    vm.TrainingCenterID))
                .ForMember(pePassport => pePassport.PEWelderID, options => options.MapFrom(vm =>
                    vm.PEWelderID));

            CreateMap<PEPassport, PEPassportEditViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(passport =>
                    _protector.Protect(passport.ID.ToString())))
                .ForMember(vm => vm.Letter, options => options.MapFrom(passport =>
                    passport.TrainingCenter.Letter))
                .ForMember(vm => vm.TrainingCenterID, options => options.MapFrom(passport =>
                    passport.TrainingCenterID))
                .ForMember(vm => vm.PEWelderID, options => options.MapFrom(passport =>
                    passport.PEWelderID));

            CreateMap<PEPassportEditViewModel, PEPassport>()
                .ForMember(passport => passport.ID, options => options.MapFrom(vm =>
                    _protector.Unprotect(vm.EncryptedID)))
                .ForMember(passport => passport.TrainingCenterID, options => options.MapFrom(vm =>
                    vm.TrainingCenterID))
                .ForMember(passport => passport.PEWelderID, options => options.MapFrom(vm =>
                    vm.PEWelderID))
                .ForMember(passport => passport.AVNumber, options => options.MapFrom(vm =>
                    vm.AVNumber));

            CreateMap<RegistrationUIColorGroup, PEPassportDetailsRegistrationsIndexViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.Registration.ID.ToString())))
                .ForMember(vm => vm.ExpiryDate, options => options.MapFrom(group =>
                    group.Registration.ExpiryDate))
                .ForMember(vm => vm.ExamDate, options => options.MapFrom(group =>
                    group.Registration.Examination.ExamDate))
                .ForMember(vm => vm.CompanyName, options => options.MapFrom(group =>
                    group.Registration.Company.CompanyName))
                .ForMember(vm => vm.ProcessName, options => options.MapFrom(group =>
                    group.Registration.Process.ProcessName))
                .ForMember(vm => vm.RegistrationTypeName, options => options.MapFrom(group =>
                    group.Registration.RegistrationType.RegistrationTypeName))
                .ForMember(vm => vm.HasPassed, options => options.MapFrom(group =>
                    group.Registration.HasPassed))
                .ForMember(vm => vm.Color, options => options.MapFrom(group =>
                    group.UIColor.Color));
        }
    }
}
