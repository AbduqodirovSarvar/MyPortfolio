using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.Security;

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
