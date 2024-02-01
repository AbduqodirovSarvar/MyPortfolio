using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Models.ViewModels
{
    public class UserLanguageViewModel
    {
        public long Id { get; set; }
        public long LanguageId { get; private set; }
        public Language? Language { get; set; }
        public long UserId { get; private set; }
        public User? User { get; set; }
        public string? LanguageLevel { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
