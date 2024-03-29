﻿using Application.Security;
using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Profiles
{
    internal class ExamCenterProfile : Profile
    {
        private readonly IDataProtector _protector;

        public ExamCenterProfile(IDataProtectionProvider dataProtectionProvider, 
            IDataProtectionPurposeStrings dataProtectionPurposeString)
        {
            _protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeString.IdRouteValue);

            CreateMap<ExamCenter, ExamCenterIndexViewModel>()
                .ForMember(ecvm => ecvm.EncryptedID, options => options.MapFrom(ec => 
                    _protector.Protect(Convert.ToString(ec.ID))))
                .ForMember(ecvm => ecvm.CompanyName, options => options.MapFrom(ec =>
                    ec.Company.CompanyName))
                .ForMember(index => index.BusinessAddressPostalCode, options => options.MapFrom(ec =>
                    ec.Company.Address.BusinessAddressPostalCode))
                .ForMember(index => index.BusinessAddressCity, options => options.MapFrom(ec =>
                    ec.Company.Address.BusinessAddressCity))
                .ForMember(index => index.Contact, options => options.MapFrom(ec =>
                    ec.CompanyContact.Contact.FirstName + " " +
                    ec.CompanyContact.Contact.LastName))
                .ForMember(index => index.Email, options => options.MapFrom(ec =>
                    ec.CompanyContact.Email))
                .ForMember(index => index.MobilePhone, options => options.MapFrom(ec =>
                    ec.CompanyContact.MobilePhone));

            CreateMap<ExamCenter, ExamCenterDetailsViewModel>()
                .ForMember(ecvm => ecvm.EncryptedID, options => options.MapFrom(ec =>
                    _protector.Protect(Convert.ToString(ec.ID))))
                .ForMember(ecvm => ecvm.CompanyName, options => options.MapFrom(ec =>
                    ec.Company.CompanyName));
            
            CreateMap<ExamCenterCreateViewModel, ExamCenter>();

            CreateMap<ExamCenter, ExamCenterEditViewModel>()
                .ForMember(ecvm => ecvm.EncryptedID, options => options.MapFrom(ec =>
                    _protector.Protect(Convert.ToString(ec.ID))));

            CreateMap<ExamCenterEditViewModel, ExamCenter>()
                .ForMember(ec => ec.ID, options => options.MapFrom(ecvm =>
                    _protector.Unprotect(ecvm.EncryptedID)));
        }
    }
}
