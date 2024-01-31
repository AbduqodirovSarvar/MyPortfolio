using MyPortfolio.Entity.Behaviour;
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
    public class SocialViewModel
    {
        public long Id { get; set; }
        public string? SocialNetwork { get; set; }
        public string? Url { get; set; }
        public long UserId { get; set; }
        public User? User { get; set; }
    }
}
