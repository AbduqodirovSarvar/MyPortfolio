using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortfolio.Entity.Entities
{
    public sealed record UserLanguage : BaseEntity
    {
        public UserLanguage(long languageId, long userId, LanguageLevel languageLevel) : base()
        {
            LanguageId = languageId;
            UserId = userId;
            LanguageLevel = languageLevel;
        }

        public UserLanguage(long languageId, User user, LanguageLevel languageLevel) : base()
        {
            LanguageId = languageId;
            User = user;
            LanguageLevel = languageLevel;
        }

        public UserLanguage(Language language, long userId, LanguageLevel languageLevel) : base()
        {
            Language = language;
            UserId = userId;
            LanguageLevel = languageLevel;
        }

        public UserLanguage(Language language, User user, LanguageLevel languageLevel) : base()
        {
            Language = language;
            User = user;
            LanguageLevel = languageLevel;
        }

        public long LanguageId { get; private set; }
        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }
        [Required]
        public long UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public LanguageLevel LanguageLevel { get; private set; }

        public override UserLanguage Change(object obj)
        {
            Task task = (obj is UserLanguage userLanguage)
                        ? Task.Run(() =>
                        {
                            LanguageId = userLanguage.LanguageId;
                            LanguageLevel = userLanguage.LanguageLevel;
                        })
                            : throw new ArgumentException("Invalid object type for change", nameof(obj));

            return this;
        }
    }
}
