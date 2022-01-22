using Application.Security;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Profiles
{
    public class RegistrationProfile : Profile
    {
        private readonly IDataProtector _protector;

        public RegistrationProfile(IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);

            CreateMap<CertificateCreateViewModel, Registration>()
                .ForMember(registration => registration.ExaminationID, options => options.MapFrom(vm =>
                    Convert.ToInt32(_protector.Unprotect(vm.ExaminationEncryptedID))))
                .ForMember(registration => registration.PEPassportID, options => options.MapFrom(vm =>
                    vm.PEPassportID))
                .ForMember(registration => registration.ProcessID, options => options.MapFrom(vm =>
                    vm.ProcessID))
                .ForMember(registration => registration.RegistrationTypeID, options => options.MapFrom(vm =>
                    vm.RegistrationTypeID));

            CreateMap<Registration, CertificateEditViewModel>()
                .ForMember(vm => vm.Letter, options => options.MapFrom(registration =>
                    registration.PEPassport.TrainingCenter.Letter))
                .ForMember(vm => vm.AVNumber, options => options.MapFrom(registration =>
                    registration.PEPassport.AVNumber))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(registration =>
                    registration.PEPassport.PEWelder.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(registration =>
                    registration.PEPassport.PEWelder.LastName))
                .ForMember(vm => vm.TrainingCenterName, options => options.MapFrom(registration =>
                    registration.PEPassport.TrainingCenter.Company.CompanyName))
                .ForMember(vm => vm.ProcessID, options => options.MapFrom(registration =>
                    registration.Process.ID))
                // Current Certificate
                .ForMember(vm => vm.CurrentCertificateCompanyID, options => options.MapFrom(registration =>
                    registration.Company.ID))
                .ForMember(vm => vm.CurrentCertificateExamDate, options => options.MapFrom(registration =>
                    registration.Examination.ExamDate))
                .ForMember(vm => vm.CurrentCertificateExpiryDate, options => options.MapFrom(registration =>
                    registration.ExpiryDate))
                .ForMember(vm => vm.CurrentCertificateRegistrationTypeID, options => options.MapFrom(registration =>
                    registration.RegistrationType.ID))
                .ForMember(vm => vm.CurrentCertificateHasPassed, options => options.MapFrom(registration =>
                    registration.HasPassed))
                .ForMember(vm => vm.CurrentCertificateExamCenterName, options => options.MapFrom(registration =>
                    registration.Examination.ExamCenter.Company.CompanyName))
                .ForMember(vm => vm.CurrentCertificateRevokeDate, options => options.MapFrom(registration =>
                    registration.Revoke.RevokeDate))
                .ForMember(vm => vm.CurrentCertificateRevokedByCompanyContactID, options => options.MapFrom(registration =>
                    registration.Revoke.CompanyContactID))
                .ForMember(vm => vm.CurrentCertificateRevokeComment, options => options.MapFrom(registration =>
                    registration.Revoke.Comment))
                // Previous Certificate
                .ForMember(vm => vm.PreviousCertificateExpiryDate, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.ExpiryDate))
                .ForMember(vm => vm.PreviousCertificateHasPassed, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.HasPassed))
                .ForMember(vm => vm.PreviousCertificateRevokeDate, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.Revoke.RevokeDate))
                .ForMember(vm => vm.PreviousCertificateRevokedBy, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.Revoke.CompanyContact.Contact.FirstName + " " +
                    registration.PreviousRegistration.Revoke.CompanyContact.Contact.LastName))
                .ForMember(vm => vm.PreviousCertificateRevokeComment, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.Revoke.Comment));

            CreateMap<Registration, CertificateDetailsViewModel>()
                .ForMember(vm => vm.AVNumber, options => options.MapFrom(registration =>
                    registration.PEPassport.TrainingCenter.Letter + registration.PEPassport.AVNumber))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(registration =>
                    registration.PEPassport.PEWelder.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(registration =>
                    registration.PEPassport.PEWelder.LastName))
                .ForMember(vm => vm.TrainingCenterName, options => options.MapFrom(registration =>
                    registration.PEPassport.TrainingCenter.Company.CompanyName))
                .ForMember(vm => vm.ProcessName, options => options.MapFrom(registration =>
                    registration.Process.ProcessName))
                // Current Certificate
                .ForMember(vm => vm.CurrentCertificateCompanyName, options => options.MapFrom(registration =>
                    registration.Company.CompanyName))
                .ForMember(vm => vm.CurrentCertificateExamDate, options => options.MapFrom(registration =>
                    registration.Examination.ExamDate))
                .ForMember(vm => vm.CurrentCertificateExpiryDate, options => options.MapFrom(registration =>
                    registration.ExpiryDate))
                .ForMember(vm => vm.CurrentCertificateRegistrationTypeName, options => options.MapFrom(registration =>
                    registration.RegistrationType.RegistrationTypeName))
                .ForMember(vm => vm.CurrentCertificateHasPassed, options => options.MapFrom(registration =>
                    registration.HasPassed))
                .ForMember(vm => vm.CurrentCertificateRevokeDate, options => options.MapFrom(registration =>
                    registration.Revoke.RevokeDate))
                .ForMember(vm => vm.CurrentCertificateExamCenterName, options => options.MapFrom(registration =>
                    registration.Examination.ExamCenter.Company.CompanyName))
                .ForMember(vm => vm.CurrentCertificateRevokedBy, options => options.MapFrom(registration =>
                    registration.Revoke.CompanyContact.Contact.FirstName + " " +
                    registration.Revoke.CompanyContact.Contact.LastName))
                .ForMember(vm => vm.CurrentCertificateRevokeComment, options => options.MapFrom(registration =>
                    registration.Revoke.Comment))
                // Previous Certificate
                .ForMember(vm => vm.PreviousCertificateExpiryDate, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.ExpiryDate))
                .ForMember(vm => vm.PreviousCertificateHasPassed, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.HasPassed))
                .ForMember(vm => vm.PreviousCertificateRevokeDate, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.Revoke.RevokeDate))
                .ForMember(vm => vm.PreviousCertificateRevokedBy, options => options.MapFrom(registration =>
                    registration.Revoke.CompanyContact.Contact.FirstName + " " + 
                    registration.Revoke.CompanyContact.Contact.LastName))
                .ForMember(vm => vm.PreviousCertificateRevokeComment, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.Revoke.Comment));
        }
    }
}
