using Application.ViewModels;
using AutoMapper;
using Domain.Models;

namespace Application.Profiles
{
    public class UserToApproveProfile : Profile
    {
        public UserToApproveProfile()
        {
            CreateMap<UserToApprove, UserToApproveViewModel>().ReverseMap();

            CreateMap<UserToApprove, UserToApproveIndex>().ReverseMap();

            CreateMap<UserToApproveIndex, UserToApproveViewModel>().ReverseMap();

            CreateMap<UserToApprove, UserRegistrationViewModel>().ReverseMap();

            CreateMap<UserToApproveIndex, UserToApproveIndexViewModel>().ReverseMap();
        }
    }
}
