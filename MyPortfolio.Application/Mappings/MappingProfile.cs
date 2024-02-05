using AutoMapper;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Gender, y => y.MapFrom(z => z.Gender.ToString()))
                .ForMember(x => x.Skills, y => y.MapFrom(z => z.Skills.Select(skill => skill.Skill)))
                .ReverseMap();
            CreateMap<UserLanguage, UserLanguageViewModel>()
                .ForMember(x => x.LanguageLevel, y => y.MapFrom(z => z.LanguageLevel.ToString()))
                .ReverseMap();
            CreateMap<Social, SocialViewModel>()
                .ForMember(x => x.SocialNetwork, y => y.MapFrom(z => z.SocialNetwork.ToString()))
                .ReverseMap();
            CreateMap<Experience, ExperienceViewModel>()
                .ForMember(x => x.WorkType, y => y.MapFrom(z => z.WorkType.ToString()))
                .ForMember(x => x.Skills, y => y.MapFrom(z => z.Skills.Select(skill => skill.Skill)))
                .ReverseMap();
            CreateMap<Skill, SkillViewModel>()
                .ReverseMap();
            CreateMap<Certificate, CertificateViewModel>()
                .ForMember(x => x.Skills, y => y.MapFrom(z => z.Skills.Select(skill => skill.Skill)))
                .ReverseMap();
            CreateMap<Project, ProjectViewModel>()
                .ForMember(x => x.Skills, y => y.MapFrom(z => z.Skills.Select(skill => skill.Skill)))
                .ReverseMap();
            CreateMap<Education, EducationViewModel>()
                .ReverseMap();
            CreateMap<Language, LanguageViewModel>()
                .ReverseMap();
                
        }
    }
}
