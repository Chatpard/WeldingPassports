using Application.Security;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Linq;

namespace Application.Profiles
{
    public class ContactProfile : Profile
    {
        private readonly IDataProtector _protector;

        public ContactProfile(IDataProtectionProvider dataProtectionProvider,
            IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);

            CreateMap<Contact, ContactIndexViewModel>()
                .ForMember(index => index.EncryptedID, options => options.MapFrom(companyContact =>
                    _protector.Protect(companyContact.ID.ToString())));

            CreateMap<Contact, ContactDetailsViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.ID.ToString())));

            CreateMap<ContactCreateViewModel, Contact>();

            CreateMap<Contact, ContactEditViewModel>()
                .ForMember(vm => vm.EncryptedID, options => options.MapFrom(group =>
                    _protector.Protect(group.ID.ToString())));

            CreateMap<ContactEditViewModel, Contact>()
                .ForMember(vm => vm.ID, options => options.MapFrom(group =>
                    _protector.Unprotect(group.EncryptedID)));
        }
    }
}
