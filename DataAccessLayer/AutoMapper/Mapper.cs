using AutoMapper;
using Management.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Management.Common.Models.DTO;
using Management.Common.Models.Entity;
using Management.Common.Enum;
using Microsoft.AspNetCore.Http;

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


            CreateMap<ProjectEntity, ProjectDto>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))


                .ForMember(dest => dest.DocumentationUrl, opt => opt.MapFrom(src => src.Documents.FirstOrDefault(x => x.ProjectEntityId == src.Id && x.DocumentType == DocumentType.Documentation).FilePath))

                .ForMember(dest => dest.SnapShootsUrl, opt => opt.MapFrom(src => src.Documents.Where(x => x.ProjectEntityId == src.Id && x.DocumentType == DocumentType.SnapShoots).Select(x => x.FilePath).ToList()))

                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.Documents.FirstOrDefault(x => x.ProjectEntityId == src.Id && x.DocumentType == DocumentType.Logo).FilePath))

                .ForMember(dest => dest.TechStackUsedObj, opt => opt.MapFrom(src => src.TechStackUsed.Where(x => x.ProjectEntityId == src.Id).Select(y => new TechStackDto { Id = y.Id, Name = y.TechStack.TechStackName }).ToList()));
            
        }

    }
}
