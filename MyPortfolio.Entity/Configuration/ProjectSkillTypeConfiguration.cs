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
    public class ProjectSkillTypeConfiguration : IEntityTypeConfiguration<ProjectSkill>
    {
        public void Configure(EntityTypeBuilder<ProjectSkill> builder)
        {
            builder.HasKey(x => new {x.ProjectId, x.SkillId});
            builder.HasOne(x => x.Skill).WithMany(x => x.Projects).HasForeignKey(x => x.SkillId);
            builder.HasOne(x => x.Project).WithMany(x => x.Skills).HasForeignKey(x => x.ProjectId);
        }
    }
}
