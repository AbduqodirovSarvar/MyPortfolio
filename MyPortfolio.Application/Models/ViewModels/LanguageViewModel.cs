using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Models.ViewModels
{
    public class LanguageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
    }
}
