using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateCreate;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateDelete;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IActionResult> CreateCertificate([FromBody] CreateCertificateCommand command)
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
        public async Task<IActionResult> UpdateCertificate([FromBody] UpdateCertificateCommand command)
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

        [HttpDelete("{certificateId")]
        public async Task<IActionResult> DeleteCertificate([FromBody] long certificateId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteCertificateCommand(certificateId)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
