using MediatR;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationCreate
{
    public sealed class CreateEducationCommand : IRequest<Education>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string EducationWebSiteUrl { get; set; } = null!;
    }
}
