﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.UserCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.UserDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.UserUpdate;
using MyPortfolio.Application.UseCases.ToDoUser.Queries;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ApiController
    {
        public UserController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand command)
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

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserCommand command)
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteUserCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            try
            {
                return Ok(await _mediator.Send(new GetUserQuery(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("mail/{email}")]
        public async Task<IActionResult> GetByEmail([FromRoute] string email)
        {
            try
            {
                return Ok(await _mediator.Send(new GetUserQuery(email)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetUsersByFilterQuery()));
        }
    }
}
