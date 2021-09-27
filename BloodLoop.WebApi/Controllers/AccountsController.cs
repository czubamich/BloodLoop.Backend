using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BloodCore.Common;
using BloodLoop.Application.Accounts.Commands;
using BloodLoop.Application.Shared;
using MediatR;

namespace BloodLoop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult<DonorDto>> RegisterDonor([FromBody] RegisterDonorCommand command, CancellationToken cancellationToken)
            => (await _mediator.Send(command, cancellationToken)).ToActionResult();
    }
}
