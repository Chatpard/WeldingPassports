using Application.ViewModels;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Profiles
{
    public class AppSettingsProfile : Profile
    {
        public AppSettingsProfile()
        {
            CreateMap<AppSettings, AppSettingsViewModel>();
            CreateMap<AppSettingsViewModel, AppSettings>();
        }
    }
}
