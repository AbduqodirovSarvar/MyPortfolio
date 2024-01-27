using MyPortfolio.Entity.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Entity.Entities
{
    public sealed record Language : BaseEntity
    {
        public Language(string name)
            :base()
        {
            Name = name;
        }

        [Required]
        [MaxLength(255)]
        public string Name { get; private set; }
        public ICollection<UserLanguage> Users { get; set; } = new HashSet<UserLanguage>();
        public override Language Change(object obj)
        {
            Task task = (obj is Language language)
                        ? Task.Run(() =>
                        {
                            Name = language.Name ?? Name;
                        })
                            : throw new ArgumentException("Invalid object type for change", nameof(obj));

            return this;
        }
    }
}
