using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationUpdate;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/users/setting/education")]
    [ApiController]
    [Authorize]
    public class UserEducationController : ApiController
    {
        public UserEducationController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateEducation([FromBody] CreateEducationCommand command)
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
        public async Task<IActionResult> UpdateEducation([FromBody] UpdateEducationCommand command)
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
        public async Task<IActionResult> DeleteEducation(long id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteEducationCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
