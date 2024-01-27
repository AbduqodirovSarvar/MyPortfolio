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
    public sealed class ExperienceSkillTypeConfiguration : IEntityTypeConfiguration<ExperienceSkill>
    {
        public void Configure(EntityTypeBuilder<ExperienceSkill> builder)
        {
            builder.HasKey(x => new { x.SkillId, x.ExperienceId });
            builder.HasOne(x => x.Experience).WithMany(x => x.Skills).HasForeignKey(x => x.ExperienceId);
            builder.HasOne(x => x.Skill).WithMany(x => x.Experiences).HasForeignKey(x => x.SkillId);

        }
    }
}
