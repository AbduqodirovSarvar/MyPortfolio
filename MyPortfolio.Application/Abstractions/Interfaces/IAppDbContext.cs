using Microsoft.EntityFrameworkCore;
using MyPortfolio.Entity.Entities;

namespace MyPortfolio.Application.Abstractions.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserLanguage> UserLanguages { get; set; }
        DbSet<Certificate> Certificates { get; set; }
        DbSet<CertificateSkill> CertificateSkills { get; set; }
        DbSet<Education> Educations { get; set; }
        DbSet<Experience> Experiences { get; set; }
        DbSet<ExperienceSkill> ExperienceSkills { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<ProjectSkill> ProjectSkills { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<Social> Socials { get; set; }
        DbSet<UserSkill> UserSkills { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
