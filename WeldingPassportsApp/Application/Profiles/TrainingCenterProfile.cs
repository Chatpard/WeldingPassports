using Application.Security;
using Application.SQLModels;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Linq;

namespace Application.Profiles
{
    public class TrainingCenterProfile : Profile
    {
        private readonly IDataProtector _protector;

        public TrainingCenterProfile(IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);

            CreateMap<TrainingCenter, TrainingCenterIndexViewModel>()
                .ForMember(index => index.EncryptedId, options => options.MapFrom(trainingCenter =>
                    _protector.Protect(trainingCenter.ID.ToString())))
                .ForMember(index => index.CompanyName, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.CompanyName))
                .ForMember(index => index.BusinessAddressPostalCode, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddressPostalCode))
                .ForMember(index => index.BusinessAddressCity, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddressCity))
                .ForMember(index => index.Contact, options => options.MapFrom(trainingCenter =>
                    trainingCenter.ListTrainingCenter.CompanyContact.Contact.FirstName + " " +
                    trainingCenter.ListTrainingCenter.CompanyContact.Contact.LastName))
                .ForMember(index => index.Email, options => options.MapFrom(trainingCenter =>
                    trainingCenter.ListTrainingCenter.CompanyContact.Email))
                .ForMember(index => index.MobilePhone, options => options.MapFrom(trainingCenter =>
                    trainingCenter.ListTrainingCenter.CompanyContact.MobilePhone));

            CreateMap<TrainingCenter, TrainingCenterDetailsViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(trainingCenter =>
                    _protector.Protect(trainingCenter.ID.ToString())))
                .ForMember(vm => vm.Letter, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Letter))
                .ForMember(vm => vm.IsActive, options => options.MapFrom(trainingCenter =>
                    trainingCenter.IsActive))
                .ForMember(vm => vm.CompanyName, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.CompanyName))
                .ForMember(vm => vm.CompanyMainPhone, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.CompanyMainPhone))
                .ForMember(vm => vm.CompanyEmail, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.CompanyEmail))
                .ForMember(vm => vm.WebPage, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.WebPage))
                // Training Center Address
                .ForMember(vm => vm.TrainingCenterAddress, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddress))
                .ForMember(vm => vm.TrainingCenterAddressCity, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddressCity))
                .ForMember(vm => vm.TrainingCenterAddressPostalCode, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddressPostalCode))
                .ForMember(vm => vm.TrainingCenterAddressCountry, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddressCountry))
                // Training Center Contact Information
                .ForMember(vm => vm.FirstName, options => options.MapFrom(trainingCenter =>
                    trainingCenter.ListTrainingCenter.CompanyContact.Contact.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(trainingCenter =>
                        trainingCenter.ListTrainingCenter.CompanyContact.Contact.LastName))
                .ForMember(vm => vm.Birthday, options => options.MapFrom(trainingCenter =>
                        trainingCenter.ListTrainingCenter.CompanyContact.Contact.Birthday))
                .ForMember(vm => vm.Email, options => options.MapFrom(trainingCenter =>
                        trainingCenter.ListTrainingCenter.CompanyContact.Email))
                .ForMember(vm => vm.JobTitle, options => options.MapFrom(trainingCenter =>
                        trainingCenter.ListTrainingCenter.CompanyContact.JobTitle))
                .ForMember(vm => vm.BusinessPhone, options => options.MapFrom(trainingCenter =>
                        trainingCenter.ListTrainingCenter.CompanyContact.BusinessPhone))
                .ForMember(vm => vm.MobilePhone, options => options.MapFrom(trainingCenter =>
                        trainingCenter.ListTrainingCenter.CompanyContact.MobilePhone))
                // Business Address
                .ForMember(vm => vm.BusinessAddress, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddress))
                .ForMember(vm => vm.BusinessAddressCity, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddressCity))
                .ForMember(vm => vm.BusinessAddressPostalCode, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddressPostalCode))
                .ForMember(vm => vm.BusinessAddressCountry, options => options.MapFrom(trainingCenter =>
                    trainingCenter.Company.Address.BusinessAddressCountry));

            CreateMap<TrainingCenter, TrainingCenterEditViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(trainingCenter =>
                    _protector.Protect(trainingCenter.ID.ToString())))
                .ForMember(vm => vm.CompanyContactID, options => options.MapFrom(trainingCenter =>
                    trainingCenter.ListTrainingCenter.CompanyContactID));

            CreateMap<TrainingCenterEditViewModel, TrainingCenter>()
                .ForMember(trainingCenter => trainingCenter.ID, options => options.MapFrom(vm =>
                    _protector.Unprotect(vm.EncryptedID)));

            CreateMap<TrainingCenterCreateViewModel, TrainingCenter>();
        }
    }
}
