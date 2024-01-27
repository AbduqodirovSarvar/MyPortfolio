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
    public sealed class UserSkillTypeConfiguration : IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {
            builder.HasKey(x => new {x.SkillId, x.UserId});
            builder.HasOne(x => x.Skill).WithMany(x => x.Users).HasForeignKey(x => x.SkillId);
            builder.HasOne(x => x.User).WithMany(x => x.Skills).HasForeignKey(x => x.UserId);
        }
    }
}
