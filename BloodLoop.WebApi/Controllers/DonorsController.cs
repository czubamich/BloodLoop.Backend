using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BloodCore.Common;
using BloodLoop.Application.Accounts;
using BloodLoop.Application.Accounts.Queries;
using BloodLoop.Application.Donations.Commands;
using BloodLoop.Domain.Accounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BloodLoop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DonorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Info")]
        [Authorize(Roles = nameof(Role.Donor))]
        public async Task<ActionResult<DonorDto>> GetCurrentDonorInfo(CancellationToken cancellationToken)
            => (await _mediator.Send(new GetCurrentDonorInfoQuery(), cancellationToken)).ToActionResult();

        [HttpGet("Donor")]
        [Authorize]
        public async Task<ActionResult<DonorDto>> GetCurrentDonorInfo([FromBody] GetCurrentDonorInfoQuery query, CancellationToken cancellationToken)
            => (await _mediator.Send(query, cancellationToken)).ToActionResult();
    }
}
