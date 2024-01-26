using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Behaviour;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Entities
{
    public sealed record Education : BaseEntity
    {
        public Education(
            string name,
            string description,
            string city,
            DateOnly fromDate,
            DateOnly toDate,
            string educationWebSiteUrl,
            long userId
            ):base() 
        {
            Name = name;
            Description = description;
            City = city;
            FromDate = fromDate;
            ToDate = toDate;
            EducationWebSiteUrl = educationWebSiteUrl;
            UserId = userId;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string City { get; private set; }
        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }
        [UriValidation]
        public string EducationWebSiteUrl { get; private set; }
        [Required]
        public long UserId {  get; private set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
