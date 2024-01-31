using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Models.ViewModels
{
    public sealed class UserViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly BirthDay { get; set; }
        public string? Gender { get; set; }
        public string? Profession { get; set; }
        public string? AboutMe { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhotoUrl { get; set; }
        public string? ResumeUrl { get; private set; }
        public ICollection<UserSkill> Skills { get; set; } = new HashSet<UserSkill>();
        public ICollection<UserLanguageViewModel> Languages { get; set; } = new HashSet<UserLanguageViewModel>();
        public ICollection<Certificate> Certificates { get; set; } = new HashSet<Certificate>();
        public ICollection<ExperienceViewModel> Experiences { get; set; } = new HashSet<ExperienceViewModel>();
        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public ICollection<SocialViewModel> Socials { get; set; } = new HashSet<SocialViewModel>();
        public ICollection<Education> Educations { get; set; } = new HashSet<Education>();
    }
}
