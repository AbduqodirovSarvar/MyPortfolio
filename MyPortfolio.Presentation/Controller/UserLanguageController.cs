using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageUpdate;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/users/setting/language")]
    [ApiController]
    [Authorize]
    public class UserLanguageController : ApiController
    {
        public UserLanguageController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLanguageCommand command)
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
        public async Task<IActionResult> Update([FromBody] UpdateLanguageCommand command)
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
                return Ok(await _mediator.Send(new DeleteLanguageCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
