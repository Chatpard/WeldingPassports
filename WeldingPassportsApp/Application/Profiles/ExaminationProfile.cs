using Application.Security;
using Application.SQLModels;
using Application.ViewModels;
using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Linq;

namespace Application.Profiles
{
    public class ExaminationProfile : Profile
    {
        private readonly IDataProtector _protector;

        public ExaminationProfile(IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);

            CreateMap<Examination, ExaminationEditViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(examination =>
                    _protector.Protect(examination.ID.ToString())))
                .ForMember(vm => vm.ExamCenterID, options => options.MapFrom(examination =>
                    examination.ExamCenter.ID))
                .ForMember(vm => vm.TrainingCenterID, options => options.MapFrom(examination =>
                    examination.TrainingCenter.ID))
                .ReverseMap()
                .ForMember(examination => examination.ID, options => options.MapFrom(vm =>
                    _protector.Unprotect(vm.EncryptedID)))
                .ForMember(examination => examination.ExamCenterID, options => options.MapFrom(vm =>
                    vm.ExamCenterID))
                .ForMember(examination => examination.TrainingCenterID, options => options.MapFrom(vm =>
                    vm.TrainingCenterID));

            CreateMap<ExaminationCreateViewModel, Examination>();

            CreateMap<Examination, ExaminationIndexViewModel>()
                .ForMember(index => index.EncryptedID, options => options.MapFrom(examination =>
                    _protector.Protect(examination.ID.ToString())))
                .ForMember(index => index.ExamDate, options => options.MapFrom(examination =>
                    examination.ExamDate))
                .ForMember(index => index.CompanyName, options => options.MapFrom(examination =>
                    examination.TrainingCenter.Company.CompanyName))
                .ForMember(index => index.NumberOfPassports, options => options.MapFrom(examination =>
                    examination.Registrations.Count));

            CreateMap<ExaminationTrainingCenterPEPassportPEWelderRegistrationUIColorsGroup, ExaminationDetailsViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.Examination.ID.ToString())))
                .ForMember(vm => vm.ExamDate, options => options.MapFrom(group =>
                    group.Examination.ExamDate))
                .ForMember(vm => vm.ExamCenterName, options => options.MapFrom(group =>
                    group.Examination.ExamCenter.Company.CompanyName))
                .ForMember(vm => vm.TrainingCenterName, options => options.MapFrom(group =>
                    group.Examination.TrainingCenter.Company.CompanyName))
                .ForMember(vm => vm.Certifications, options => options.MapFrom(group =>
                    group.PEPassportPEWelderRegistrationUIColors));

            CreateMap<RegistrationUIColorGroup, ExaminationDetailsCertificationsIndexViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.Registration.ID.ToString())))
                .ForMember(vm => vm.Letter, options => options.MapFrom(group =>
                    group.Registration.PEPassport.TrainingCenter.Letter))
                .ForMember(vm => vm.AVNumber, options => options.MapFrom(group =>
                    group.Registration.PEPassport.AVNumber))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(group =>
                    group.Registration.PEPassport.PEWelder.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(group =>
                    group.Registration.PEPassport.PEWelder.LastName))
                .ForMember(vm =>vm.ProcessName, options => options.MapFrom(group =>
                    group.Registration.Process.ProcessName))
                .ForMember(vm => vm.RegistrationTypeName, options => options.MapFrom(group =>
                    group.Registration.RegistrationType.RegistrationTypeName))
                .ForMember(vm => vm.HasPassed, options => options.MapFrom(group =>
                    group.Registration.HasPassed))
                .ForMember(index => index.Color, options => options.MapFrom(group =>
                    group.UIColor.Color));
        }
    }
}
