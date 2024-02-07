using MyPortfolio.Entity.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Entity.Entities
{
    public sealed record ProjectSkill : BaseEntity
    {
        public ProjectSkill(long skillId, long projectId) : base()
        {
            SkillId = skillId;
            ProjectId = projectId;
        }
        public ProjectSkill(long skillId, Project project) : base()
        {
            SkillId = skillId;
            Project = project;
        }
        public ProjectSkill(Skill skill, long projectId) : base()
        {
            Skill = skill;
            ProjectId = projectId;
        }
        public ProjectSkill(Skill skill, Project project) : base()
        {
            Skill = skill;
            Project = project;
        }
        public long SkillId { get; set; }
        [ForeignKey(nameof(SkillId))]
        public Skill? Skill { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }

        public override ProjectSkill Change(object obj)
        {
            return this;
        }
    }
}
