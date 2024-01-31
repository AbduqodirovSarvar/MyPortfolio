using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceUpdate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillDelete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/users/setting/skill")]
    [ApiController]
    [Authorize]
    public class UserSkillController : ApiController
    {
        public UserSkillController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSkillCommand command)
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
                return Ok(await _mediator.Send(new DeleteSkillCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
