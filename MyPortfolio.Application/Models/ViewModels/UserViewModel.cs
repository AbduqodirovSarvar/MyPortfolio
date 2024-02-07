namespace MyPortfolio.Application.Models.ViewModels
{
    public sealed class UserViewModel
    {
        public long Id { get; set; }
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
        public DateTime CreatedTime { get; set; }
        public ICollection<SkillViewModel> Skills { get; set; } = new HashSet<SkillViewModel>();
        public ICollection<UserLanguageViewModel> Languages { get; set; } = new HashSet<UserLanguageViewModel>();
        public ICollection<CertificateViewModel> Certificates { get; set; } = new HashSet<CertificateViewModel>();
        public ICollection<ExperienceViewModel> Experiences { get; set; } = new HashSet<ExperienceViewModel>();
        public ICollection<ProjectViewModel> Projects { get; set; } = new HashSet<ProjectViewModel>();
        public ICollection<SocialViewModel> Socials { get; set; } = new HashSet<SocialViewModel>();
        public ICollection<EducationViewModel> Educations { get; set; } = new HashSet<EducationViewModel>();
    }
}
