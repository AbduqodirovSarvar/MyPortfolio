using MyPortfolio.Entity.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    }
}
