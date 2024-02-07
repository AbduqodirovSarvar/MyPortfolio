using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyPortfolio.Entity.Entities;

namespace MyPortfolio.Entity.Configuration
{
    public class CertificateSkillTypeConfiguration : IEntityTypeConfiguration<CertificateSkill>
    {
        public void Configure(EntityTypeBuilder<CertificateSkill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.SkillId, x.CertificateId }).IsUnique();
            builder.HasOne(x => x.Certificate).WithMany(x => x.Skills).HasForeignKey(x => x.CertificateId);
            builder.HasOne(x => x.Skill).WithMany(x => x.Certificates).HasForeignKey(x => x.SkillId);
        }
    }
}
