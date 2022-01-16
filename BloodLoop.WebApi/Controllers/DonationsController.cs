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
using BloodLoop.Application.Donations.Queries.GetDonationConversion;

namespace BloodLoop.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DonationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationContext _applicationContext;

        public DonationsController(IMediator mediator, IApplicationContext applicationContext)
        {
            _mediator = mediator;
            _applicationContext = applicationContext;
        }

        [HttpGet("Interval/{fromType}/{toType}")]
        public async Task<ActionResult<TimeSpan>> GetDonationInterval([FromRoute] GetDonationIntervalQuery request, CancellationToken cancellationToken)
            => (await _mediator.Send(request, cancellationToken)).ToActionResult();

        [HttpGet("Interval/{toType}")]
        public async Task<ActionResult<TimeSpan>> GetUserDonationInterval([FromRoute] string donationTypeLabel, CancellationToken cancellationToken)
            => (await _mediator.Send(new GetDonationIntervalForUserQuery(_applicationContext.AccountId, donationTypeLabel), cancellationToken)).ToActionResult();

        [HttpGet("Conversion/{fromType}/{toType}")]
        public async Task<ActionResult<double>> GetDonationConversion([FromRoute] GetDonationConversionQuery request, CancellationToken cancellationToken)
            => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    }
}
