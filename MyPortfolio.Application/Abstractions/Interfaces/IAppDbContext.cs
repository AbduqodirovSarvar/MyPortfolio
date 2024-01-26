using Microsoft.EntityFrameworkCore;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Abstractions.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserLanguage> UserLanguages { get; set; }
        DbSet<Certificate> Certificates { get; set; }
        DbSet<Education> Educations { get; set; }
        DbSet<Experience> Experiences { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<Social> Socials { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
