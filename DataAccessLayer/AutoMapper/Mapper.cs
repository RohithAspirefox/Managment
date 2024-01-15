using AutoMapper;
using Management.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Management.Data.AutoMapper
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<SignUpDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName!))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserName!))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserName!))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email!))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => true));
        }
    }
}
