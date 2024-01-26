using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Behaviour;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Entities
{
    public sealed record Project : BaseEntity
    {
        public Project(
            string name,
            string description,
            string photoUrl,
            long userId,
            string urlToCode,
            string urlToSite
            ) :base() 
        {
            Name = name;
            Description = description;
            PhotoUrl = photoUrl;
            UserId = userId;
            UrlToCode = urlToCode;
            UrlToSite = urlToSite;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        [UriValidation]
        public string PhotoUrl { get; private set; }
        public long UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        [UriValidation]
        public string UrlToCode { get; private set; }
        [UriValidation]
        public string UrlToSite {  get; private set; }
        public ICollection<Skill> Skills { get; set; } = new HashSet<Skill>();
    }
}
