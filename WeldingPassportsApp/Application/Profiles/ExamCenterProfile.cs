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
    internal class ExamCenterProfile : Profile
    {
        private readonly IDataProtector _protector;

        public ExamCenterProfile(IDataProtectionProvider dataProtectionProvider, 
            IDataProtectionPurposeStrings dataProtectionPurposeString)
        {
            _protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeString.IdRouteValue);

            CreateMap<ExamCenter, ExamCenterIndexViewModel>()
                .ForMember(ecvm => ecvm.EncryptedID, options => options.MapFrom(ec => 
                    _protector.Protect(ec.ID.ToString())))
                .ForMember(ecvm => ecvm.CompanyName, options => options.MapFrom(ec =>
                    ec.Company.CompanyName));
        }
    }
}
