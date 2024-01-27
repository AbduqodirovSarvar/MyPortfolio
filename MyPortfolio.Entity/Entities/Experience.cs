using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyPortfolio.Entity.Entities
{
    public sealed record Experience : BaseEntity
    {
        public Experience(
            string companyName,
            string description,
            string position,
            WorkType workType,
            string city,
            DateOnly fromDate,
            DateOnly toDate,
            long userId
            ): base()
        {
            CompanyName = companyName;
            Description = description;
            Position = position;
            WorkType = workType;
            City = city;
            FromDate = fromDate;
            ToDate = toDate;
            UserId = userId;
        }
        public string CompanyName { get; private set; }
        public string Description { get; private set; }
        public string Position { get; private set; }
        public WorkType WorkType { get; private set; }
        public string City { get; private set; }
        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }
        [Required]
        public long UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public ICollection<ExperienceSkill> Skills { get; set; } = new HashSet<ExperienceSkill>();

        public override Experience Change(object obj)
        {
            Task task = (obj is Experience experience)
                        ? Task.Run(() =>
                        {
                            CompanyName = experience.CompanyName ?? CompanyName;
                            Description = experience.Description ?? Description;
                            Position = experience.Position ?? Position;
                            WorkType = experience.WorkType;
                            City = experience.City ?? City;
                            FromDate = experience.FromDate;
                            ToDate = experience.ToDate;
                        })
                            : throw new ArgumentException("Invalid object type for change", nameof(obj));

            return this;
        }
    }
}
