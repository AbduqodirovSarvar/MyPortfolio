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
    public sealed record Certificate : BaseEntity
    {
        public Certificate(
            string name,
            string decription,
            string certificateUrl,
            string credential,
            DateOnly issued,
            long userId
            )
            :base()
        {
            Name = name;
            Description = decription;
            CertificateUrl = certificateUrl;
            Credential = credential;
            Issued = issued;
            UserId = userId;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        [Required]
        [UriValidation]
        public string CertificateUrl { get; private set; }
        public string Credential {  get; private set; }
        public DateOnly Issued {  get; private set; }
        [Required]
        public long UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public ICollection<Skill> Skills { get; set; } = new HashSet<Skill>();
    }
}
