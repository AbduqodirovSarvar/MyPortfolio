using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entity.Entities;

namespace MyPortfolio.Entity.Configuration
{
    public sealed class ExperienceSkillTypeConfiguration : IEntityTypeConfiguration<ExperienceSkill>
    {
        public void Configure(EntityTypeBuilder<ExperienceSkill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.SkillId, x.ExperienceId }).IsUnique();
            builder.HasOne(x => x.Experience).WithMany(x => x.Skills).HasForeignKey(x => x.ExperienceId);
            builder.HasOne(x => x.Skill).WithMany(x => x.Experiences).HasForeignKey(x => x.SkillId);

        }
    }
}
