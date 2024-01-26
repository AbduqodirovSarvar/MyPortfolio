using MyPortfolio.Entity.Abstraction;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Entities
{
    public sealed record UserLanguage : BaseEntity
    {
        public UserLanguage(
            long languageId,
            long userId,
            LanguageLevel languageLevel
            ):base()
        {
            LanguageId = languageId;
            UserId = userId;
            LanguageLevel = languageLevel;
        }
        public long LanguageId { get; private set; }
        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }
        public long UserId { get; private set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public LanguageLevel LanguageLevel { get; private set; }
    }
}
