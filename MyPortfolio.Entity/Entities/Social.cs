using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Enums;
using System;
using System.ComponentModel.DataAnnotations;

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
    }
}
