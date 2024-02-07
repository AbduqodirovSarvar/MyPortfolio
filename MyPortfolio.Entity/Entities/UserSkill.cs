using MyPortfolio.Entity.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Entity.Entities
{
    public sealed record UserSkill : BaseEntity
    {
        public UserSkill(long skillId, long userId) : base()
        {
            SkillId = skillId;
            UserId = userId;
        }
        public UserSkill(Skill skill, long userId) : base()
        {
            Skill = skill;
            UserId = userId;
        }
        public UserSkill(long skillId, User user) : base()
        {
            SkillId = skillId;
            User = user;
        }
        public UserSkill(Skill skill, User user) : base()
        {
            Skill = skill;
            User = user;
        }
        public long SkillId { get; set; }
        [ForeignKey(nameof(SkillId))]
        public Skill? Skill { get; set; }
        [Required]
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        public override UserSkill Change(object obj)
        {
            return this;
        }
    }
}
