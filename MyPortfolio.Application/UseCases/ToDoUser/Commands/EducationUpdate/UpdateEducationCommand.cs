using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationUpdate
{
    public sealed class UpdateEducationCommand : IRequest<EducationViewModel>
    {
        [Required]
        public long Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? City { get; set; } = null;
        public DateOnly? FromDate { get; set; } = null;
        public DateOnly? ToDate { get; set; } = null;
        public string? EducationWebSiteUrl { get; set; } = null;
    }
}
