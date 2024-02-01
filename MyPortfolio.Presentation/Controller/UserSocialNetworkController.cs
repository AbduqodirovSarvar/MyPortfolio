using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceUpdate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/users/setting/socialNetwork")]
    [ApiController]
    [Authorize]
    public class UserSocialNetworkController : ApiController
    {
        public UserSocialNetworkController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSocialNetworkCommand command)
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
        public async Task<IActionResult> Update([FromBody] UpdateSocialCommand command)
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
                return Ok(await _mediator.Send(new DeleteSocialCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
