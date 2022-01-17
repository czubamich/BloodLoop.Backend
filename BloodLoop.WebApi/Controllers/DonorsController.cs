using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BloodCore.Common;
using BloodLoop.Application.Accounts;
using BloodLoop.Application.Accounts.Queries;
using BloodLoop.Application.Donations;
using BloodLoop.Application.Donations.Commands;
using BloodLoop.Application.Donations.Commands.AddDonations;
using BloodLoop.Application.Donations.Queries.GetDonations;
using BloodLoop.Application.Donations.Queries.GetDonationsSummary;
using BloodLoop.Application.Donations.Shared;
using BloodLoop.Application.Services;
using BloodLoop.Domain.Accounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BloodLoop.Application.Donations.Queries.GetDonationInterval;

namespace BloodLoop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationContext _applicationContext;

        public DonorsController(IMediator mediator, IApplicationContext applicationContext)
        {
            _mediator = mediator;
            _applicationContext = applicationContext;
        }

        [HttpGet("Info")]
        [Authorize(Roles = nameof(Role.Donor))]
        public async Task<ActionResult<DonorDto>> GetCurrentDonorInfo(CancellationToken cancellationToken)
            => (await _mediator.Send(new GetDonorInfoQuery(_applicationContext.AccountId), cancellationToken)).ToActionResult();

        [HttpGet("Donations")]
        [Authorize(Roles = nameof(Role.Donor))]
        public async Task<ActionResult<IEnumerable<DonationGroupDto>>> GetDonations(CancellationToken cancellationToken)
            => (await _mediator.Send(new GetDonationsByYearQuery(_applicationContext.AccountId), cancellationToken)).ToActionResult();

        [HttpGet("Summary")]
        [Authorize(Roles = nameof(Role.Donor))]
        public async Task<ActionResult<DonationSummaryDto>> GetDonationSummary([FromQuery] string donationType, CancellationToken cancellationToken)
            => (await _mediator.Send(new GetDonationsSummaryQuery(_applicationContext.AccountId, donationType), cancellationToken)).ToActionResult();

        [HttpGet("Interval/{toType}")]
        public async Task<ActionResult<TimeSpan>> GetUserDonationInterval([FromRoute] string toType, CancellationToken cancellationToken)
            => (await _mediator.Send(new GetDonationIntervalForUserQuery(_applicationContext.AccountId, toType), cancellationToken)).ToActionResult();

        [HttpPost("Donations")]
        [Authorize(Roles = nameof(Role.Donor))]
        public async Task<ActionResult<DonationDto[]>> AddDonation([FromBody] IEnumerable<DonationDto> donations,CancellationToken cancellationToken)
            => (await _mediator.Send(new AddDonationsCommand(_applicationContext.AccountId, donations.ToArray()), cancellationToken)).ToActionResult();
    }
}
