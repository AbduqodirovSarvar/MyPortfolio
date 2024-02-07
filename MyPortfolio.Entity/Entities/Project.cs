using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Behaviour;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Entity.Entities
{
    public sealed record Project : BaseEntity
    {
        public Project(
            string name,
            string description,
            string? photoUrl,
            long userId,
            string urlToCode,
            string urlToSite
            ) : base()
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
        public string? PhotoUrl { get; private set; }
        [Required]
        public long UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        [UriValidation]
        public string UrlToCode { get; private set; }
        [UriValidation]
        public string UrlToSite { get; private set; }
        public ICollection<ProjectSkill> Skills { get; set; } = new HashSet<ProjectSkill>();
        public override Project Change(object obj)
        {
            Task task = (obj is Project project)
                        ? Task.Run(() =>
                        {
                            Name = project.Name ?? Name;
                            Description = project.Description ?? Description;
                            PhotoUrl = project.PhotoUrl ?? PhotoUrl;
                            UrlToCode = project.UrlToCode ?? UrlToCode;
                            UrlToSite = project.UrlToSite ?? UrlToSite;
                        })
                            : throw new ArgumentException("Invalid object type for change", nameof(obj));

            return this;
        }
    }
}
