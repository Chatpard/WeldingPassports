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

            CreateMap<AddressCreateViewModel, Address>();

            CreateMap<Address, AddressDetailsViewModel>()
                .ForMember(details => details.EncryptedID, address => address.MapFrom(address => 
                    _protector.Protect(address.ID.ToString())));
        }
    }
}
