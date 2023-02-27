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
    class AddressProfile : Profile
    {
        private readonly IDataProtector _protector;

        public AddressProfile(IDataProtectionProvider dataProtectionProvider, IDataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _protector = dataProtectionProvider
                .CreateProtector(dataProtectionPurposeStrings.IdRouteValue);

            CreateMap<AddressCreateViewModel, Address>()
                .ReverseMap();

            CreateMap<Address, AddressDetailsViewModel>()
                .ForMember(details => details.EncryptedID, address => address.MapFrom(address => 
                    _protector.Protect(Convert.ToString(address.ID))));

            CreateMap<Address, AddressEditViewModel>()
                .ForMember(vm => vm.EncryptedID, address => address.MapFrom(address =>
                    _protector.Protect(Convert.ToString(address.ID))));

            CreateMap<AddressEditViewModel, Address>()
                .ForMember(address => address.ID, vm => vm.MapFrom(vm =>
                    _protector.Unprotect(vm.EncryptedID)));
        }
    }
}
