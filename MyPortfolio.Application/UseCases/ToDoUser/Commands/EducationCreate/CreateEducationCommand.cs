using MediatR;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationCreate
{
    public sealed class CreateEducationCommand : IRequest<EducationViewModel>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string EducationWebSiteUrl { get; set; } = null!;
    }
}
