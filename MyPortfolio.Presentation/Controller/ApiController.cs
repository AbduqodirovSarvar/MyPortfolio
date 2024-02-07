using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyPortfolio.Presentation.Controller
{
    public abstract class ApiController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public ApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
