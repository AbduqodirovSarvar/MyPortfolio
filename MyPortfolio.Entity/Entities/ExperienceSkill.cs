using MyPortfolio.Entity.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Entities
{
    public sealed record ExperienceSkill : BaseEntity
    {
        public ExperienceSkill(long skillId, long experienceId) : base()
        {
            SkillId = skillId;
            ExperienceId = experienceId;
        }
        public ExperienceSkill(long skillId, Experience experience) : base()
        {
            SkillId = skillId;
            Experience = experience;
        }
        public ExperienceSkill(Skill skill, long experienceId) : base()
        {
            Skill = skill;
            ExperienceId = experienceId;
        }
        public ExperienceSkill(Skill skill, Experience experience) : base()
        {
            Skill = skill;
            Experience = experience;
        }

        public long SkillId { get; private set; }
        [ForeignKey(nameof(SkillId))]
        public Skill? Skill { get; set; } 
        public long ExperienceId { get; private set; }
        [ForeignKey(nameof(ExperienceId))]
        public Experience? Experience { get; set; }

        public override ExperienceSkill Change(object obj)
        {
            return this;
        }
    }
}
