using MyPortfolio.Entity.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Entities
{
    public sealed record Skill : BaseEntity
    {
        public Skill(string name)
            :base()
        {
            Name = name;
        }

        [Required]
        [MaxLength(255)]
        public string Name { get; private set; }
        public ICollection<UserSkill> Users { get; set; } = new HashSet<UserSkill>();
        public ICollection<CertificateSkill> Certificates { get; set; } = new HashSet<CertificateSkill>();
        public ICollection<ExperienceSkill> Experiences { get; set; } = new HashSet<ExperienceSkill>();
        public ICollection<ProjectSkill> Projects { get; set; } = new HashSet<ProjectSkill>();

        public override Skill Change(object obj)
        {
            Task task = (obj is Skill skill)
                        ? Task.Run(() =>
                        {
                            Name = skill.Name ?? Name;
                        })
                            : throw new ArgumentException("Invalid object type for change", nameof(obj));

            return this;
        }
    }
}
