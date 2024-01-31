using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceUpdate
{
    public sealed class UpdateExperienceCommand : IRequest<ExperienceViewModel>
    {
        [Required]
        public long Id { get; set; }
        public string? CompanyName { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? Position { get; set; } = null;
        public string? WorkType { get; set; } = null;
        public string? City { get; set; } = null;
        public DateOnly? FromDate { get; set; } = null;
        public DateOnly? ToDate { get; set; } = null;
    }
}
