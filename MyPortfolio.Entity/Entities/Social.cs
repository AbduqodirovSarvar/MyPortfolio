using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Entity.Entities
{
    public sealed record Social : BaseEntity
    {
        public Social(
            SocialNetwork socialNetwork,
            string url
        ) : base()
        {
            SocialNetwork = socialNetwork;
            Url = url;
        }
        [Required]
        public SocialNetwork SocialNetwork { get; private set; }
        [Required]
        [UriValidation]
        public string Url { get; private set; }
        [Required]
        public long UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
