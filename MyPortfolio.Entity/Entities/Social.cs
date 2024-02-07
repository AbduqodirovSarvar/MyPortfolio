using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Xml.Linq;

namespace MyPortfolio.Entity.Entities
{
    public sealed record Social : BaseEntity
    {
        public Social(
            SocialNetwork socialNetwork,
            string url,
            long userId
        ) : base()
        {
            SocialNetwork = socialNetwork;
            Url = url;
            UserId = userId;
        }
        public Social(
            SocialNetwork socialNetwork,
            string url,
            User user
        ) : base()
        {
            SocialNetwork = socialNetwork;
            Url = url;
            User = user;
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
        public override Social Change(object obj)
        {
            Task task = (obj is Social social)
                        ? Task.Run(() =>
                        {
                            SocialNetwork = social.SocialNetwork;
                            Url = social.Url ?? Url;
                        })
                            : throw new ArgumentException("Invalid object type for change", nameof(obj));

            return this;
        }
    }
}
