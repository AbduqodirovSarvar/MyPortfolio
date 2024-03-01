using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillDelete;

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
        public async Task<IActionResult> Create([FromForm] CreateSkillCommand command)
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
        public async Task<IActionResult> Delete(long id)
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
