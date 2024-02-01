using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.Security;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationUpdate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.UserCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.UserDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.UserUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/users")]
    [ApiController]
    public class AuthController : ApiController
    {
        public AuthController(IMediator mediator)
            : base(mediator) { }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
