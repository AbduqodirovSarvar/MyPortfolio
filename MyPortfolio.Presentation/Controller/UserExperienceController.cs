using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationUpdate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/users/setting/experience")]
    [ApiController]
    [Authorize]
    public class UserExperienceController : ApiController
    {
        public UserExperienceController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExperienceCommand command)
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

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateExperienceCommand command)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] long id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteExperienceCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
