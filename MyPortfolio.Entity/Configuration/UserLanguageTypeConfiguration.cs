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
    public sealed class UserLanguageTypeConfiguration : IEntityTypeConfiguration<UserLanguage>
    {
        public void Configure(EntityTypeBuilder<UserLanguage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new {x.UserId, x.LanguageId}).IsUnique(true);
            builder.HasOne(x => x.User).WithMany(x => x.Languages).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Language).WithMany(x => x.Users).HasForeignKey(x => x.LanguageId);
        }
    }
}
