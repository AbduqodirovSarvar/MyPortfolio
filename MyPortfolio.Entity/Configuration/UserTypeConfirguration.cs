using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Configuration
{
    public sealed class UserTypeConfirguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.PhoneNumber).IsUnique();
            builder.HasIndex(x => x.ResumeUrl).IsUnique();
            builder.HasIndex(x => x.PhotoUrl).IsUnique();
            builder.HasMany(x => x.Projects).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Skills).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Certificates).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Educations).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Experiences).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Languages).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Socials).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}
