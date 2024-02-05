using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Configuration
{
    public sealed class UserSkillTypeConfiguration : IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.UserId, x.SkillId }).IsUnique(true);
            builder.HasOne(x => x.Skill).WithMany(x => x.Users).HasForeignKey(x => x.SkillId);
            builder.HasOne(x => x.User).WithMany(x => x.Skills).HasForeignKey(x => x.UserId);
        }
    }
}
