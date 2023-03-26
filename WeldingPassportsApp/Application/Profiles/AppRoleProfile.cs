using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Profiles
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile() 
        {
            CreateMap<CompanyContactEditViewModel, AppRole>()
                .ForMember(ar => ar.Id, options => options.MapFrom(vm =>
                vm.AppRoleID));
        }
    }
}
