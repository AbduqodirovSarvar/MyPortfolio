using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Behaviour;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Entity.Entities
{
    public sealed record Certificate : BaseEntity
    {
        public Certificate(
            string name,
            string description,
            string certificateUrl,
            string credential,
            DateOnly issued,
            long userId
            )
            : base()
        {
            Name = name;
            Description = description;
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
        public string Credential { get; private set; }
        public DateOnly Issued { get; private set; }
        [Required]
        public long UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public ICollection<CertificateSkill> Skills { get; set; } = new HashSet<CertificateSkill>();

        public override Certificate Change(object obj)
        {
            Task task = (obj is Certificate certificate)
                        ? Task.Run(() =>
                        {
                            Name = certificate.Name ?? Name;
                            Description = certificate.Description ?? Description;
                            CertificateUrl = certificate.CertificateUrl ?? CertificateUrl;
                            Credential = certificate.Credential ?? Credential;
                            Issued = certificate.Issued;
                        })
                            : throw new ArgumentException("Invalid object type for change", nameof(obj));

            return this;
        }
    }
}
