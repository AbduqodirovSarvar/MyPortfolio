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
        public long Id { get; private set; }
        public LanguageViewModel? Language { get; set; }
        public string? LanguageLevel { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
