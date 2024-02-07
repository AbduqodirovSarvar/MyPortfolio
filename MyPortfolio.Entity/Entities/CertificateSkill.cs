using MyPortfolio.Entity.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Entity.Entities
{
    public sealed record CertificateSkill : BaseEntity
    {
        public CertificateSkill(long skillId, long certificateId) : base()
        {
            SkillId = skillId;
            CertificateId = certificateId;
        }
        public CertificateSkill(long skillId, Certificate certificate) : base()
        {
            SkillId = skillId;
            Certificate = certificate;
        }
        public CertificateSkill(Skill skill, long certificateId) : base()
        {
            Skill = skill;
            CertificateId = certificateId;
        }
        public CertificateSkill(Skill skill, Certificate certificate) : base()
        {
            Skill = skill;
            Certificate = certificate;
        }
        public long SkillId { get; set; }
        [ForeignKey(nameof(SkillId))]
        public Skill? Skill { get; set; }
        public long CertificateId { get; set; }
        [ForeignKey(nameof(CertificateId))]
        public Certificate? Certificate { get; set; }

        public override CertificateSkill Change(object obj)
        {
            return this;
        }
    }
}
