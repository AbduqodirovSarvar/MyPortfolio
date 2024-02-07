using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectUpdate;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/users/setting/project")]
    [ApiController]
    [Authorize]
    public class UserProjectController : ApiController
    {
        public UserProjectController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProjectCommand command)
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
        public async Task<IActionResult> Update([FromForm] UpdateProjectCommand command)
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
                return Ok(await _mediator.Send(new DeleteProjectCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
