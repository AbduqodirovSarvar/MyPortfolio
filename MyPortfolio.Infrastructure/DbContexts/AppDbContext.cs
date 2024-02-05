using Microsoft.EntityFrameworkCore;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Configuration;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Infrastructure.DbContexts
{
    public sealed class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CertificateSkill> CertificateSkills { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<ExperienceSkill> ExperienceSkills {  get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserTypeConfirguration());
            modelBuilder.ApplyConfiguration(new UserSkillTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserLanguageTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SocialTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SkillTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectSkillTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExperienceTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExperienceSkillTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EducationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CertificateTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CertificateSkillTypeConfiguration());

            modelBuilder.ApplyAllConfigurations();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
