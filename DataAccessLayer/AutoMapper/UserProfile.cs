using AutoMapper;
using Management.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Data.AutoMapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserEdit, User>()
             .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
             .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Mobile))
             .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ProfileImage))
           
             .ForMember(dest => dest.GithubURL, opt => opt.MapFrom(src => src.Github!))
             .ForMember(dest => dest.InstagramURL, opt => opt.MapFrom(src => src.Instagram!))
             .ForMember(dest => dest.GithubURL, opt => opt.MapFrom(src => src.Twitter!))
             .ForMember(dest => dest.FacebookURL, opt => opt.MapFrom(src => src.Facebook!))
             .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true))
             .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => true))
             .ForMember(dest => dest.IsLogged, opt => opt.MapFrom(src => true))
             .ForMember(dest => dest.Active, opt => opt.MapFrom(src => "Yes"));
        }
    }
}
