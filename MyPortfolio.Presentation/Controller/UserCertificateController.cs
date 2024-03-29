﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateUpdate;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/users/setting/certificate")]
    [ApiController]
    [Authorize]
    public class UserCertificateController : ApiController
    {
        public UserCertificateController(IMediator mediator)
            : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateCertificate([FromForm] CreateCertificateCommand command)
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
        public async Task<IActionResult> UpdateCertificate([FromForm] UpdateCertificateCommand command)
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
        public async Task<IActionResult> DeleteCertificate(long id)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteCertificateCommand(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
