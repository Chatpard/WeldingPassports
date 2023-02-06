using Application.Interfaces;
using Application.Interfaces.Repositories.SQL;
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
                    registration.PEPassport != null ?
                        registration.PEPassport.TrainingCenter != null ?
                            registration.PEPassport.TrainingCenter.Letter : 
                            '?' :
                        '?'))
                .ForMember(vm => vm.AVNumber, options => options.MapFrom(registration =>
                    registration.PEPassport != null ?
                        registration.PEPassport.AVNumber :
                        0))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(registration =>
                    registration.PEPassport != null ?
                        registration.PEPassport.PEWelder != null ?
                            registration.PEPassport.PEWelder.FirstName :
                            null :
                        null))
                .ForMember(vm => vm.LastName, options => options.MapFrom(registration =>
                    registration.PEPassport != null ?
                        registration.PEPassport.PEWelder != null ?
                            registration.PEPassport.PEWelder.LastName :
                            null :
                        null))
                .ForMember(vm => vm.TrainingCenterName, options => options.MapFrom(registration =>
                    registration.PEPassport != null ?
                        registration.PEPassport.TrainingCenter != null ?
                            registration.PEPassport.TrainingCenter.Company != null ?
                                registration.PEPassport.TrainingCenter.Company.CompanyName :
                                null :
                            null :
                        null))
                .ForMember(vm => vm.ProcessID, options => options.MapFrom(registration =>
                    registration.Process != null ? 
                        registration.Process.ID : 
                        0))
                // Current Certificate
                .ForMember(vm => vm.CurrentCertificateCompanyID, options => options.MapFrom(registration =>
                    registration.Company != null ?
                        registration.Company.ID :
                        0))
                .ForMember(vm => vm.CurrentCertificateExamDate, options => options.MapFrom(registration =>
                    registration.Examination != null ?
                        registration.Examination.ExamDate :
                        null))
                .ForMember(vm => vm.CurrentCertificateExpiryDate, options => options.MapFrom(registration =>
                    registration.ExpiryDate))
                .ForMember(vm => vm.CurrentCertificateRegistrationTypeID, options => options.MapFrom(registration =>
                    registration.RegistrationType != null ?
                        registration.RegistrationType.ID :
                        0))
                .ForMember(vm => vm.CurrentCertificateHasPassed, options => options.MapFrom(registration =>
                    registration.HasPassed))
                .ForMember(vm => vm.CurrentCertificateExamCenterName, options => options.MapFrom(registration =>
                    registration.Examination != null ?
                        registration.Examination.ExamCenter != null ?
                            registration.Examination.ExamCenter.Company != null ?
                                registration.Examination.ExamCenter.Company.CompanyName :
                                null :
                            null :
                        null))
                .ForMember(vm => vm.CurrentCertificateRevokeDate, options => options.MapFrom(registration =>
                    registration.Revoke != null ?
                        registration.Revoke.RevokeDate :
                        null))
                .ForMember(vm => vm.CurrentCertificateRevokedByCompanyContactID, options => options.MapFrom(registration =>
                    registration.Revoke != null ?
                        (int?) registration.Revoke.CompanyContactID :
                        null))
                .ForMember(vm => vm.CurrentCertificateRevokeComment, options => options.MapFrom(registration =>
                    registration.Revoke != null ?
                        (string?) registration.Revoke.Comment :
                        null))
                // Previous Certificate
                .ForMember(vm => vm.PreviousCertificateExpiryDate, options => options.MapFrom(registration =>
                    registration.PreviousRegistration != null ? 
                        registration.PreviousRegistration.ExpiryDate : 
                        null))
                .ForMember(vm => vm.PreviousCertificateHasPassed, options => options.MapFrom(registration =>
                    registration.PreviousRegistration != null ? 
                        registration.PreviousRegistration.HasPassed : 
                        null))
                .ForMember(vm => vm.PreviousCertificateRevokeDate, options => options.MapFrom(registration =>
                    registration.PreviousRegistration != null ? 
                        registration.PreviousRegistration.Revoke != null ? 
                            registration.PreviousRegistration.Revoke.RevokeDate :
                            null :
                        null))
                .ForMember(vm => vm.PreviousCertificateRevokedBy, options => options.MapFrom(registration =>
                    registration.PreviousRegistration != null ?
                        registration.PreviousRegistration.Revoke != null ?
                            registration.PreviousRegistration.Revoke.CompanyContact != null ?
                            registration.PreviousRegistration.Revoke.CompanyContact.Contact != null ?
                                    registration.PreviousRegistration.Revoke.CompanyContact.Contact.FirstName + " " +
                                    registration.PreviousRegistration.Revoke.CompanyContact.Contact.LastName : 
                                    "" :
                                "" :
                            "" :
                        ""))
                .ForMember(vm => vm.PreviousCertificateRevokeComment, options => options.MapFrom(registration =>
                    registration.PreviousRegistration != null ?
                        registration.PreviousRegistration.Revoke != null ?
                            registration.PreviousRegistration.Revoke.Comment : 
                            "" :
                        ""));

            CreateMap<Registration, CertificateDetailsViewModel>()
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
                    registration.PreviousRegistration.Revoke.CompanyContact.Contact.FirstName + " " + 
                    registration.PreviousRegistration.Revoke.CompanyContact.Contact.LastName))
                .ForMember(vm => vm.PreviousCertificateRevokeComment, options => options.MapFrom(registration =>
                    registration.PreviousRegistration.Revoke.Comment));
        }
    }
}
