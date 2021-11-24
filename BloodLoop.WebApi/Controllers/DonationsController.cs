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

namespace BloodLoop.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DonationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DonationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Interval/{from}/{to}")]
        public async Task<ActionResult<DonorDto>> GetDonationInterval([FromRoute] GetDonationIntervalQuery request, CancellationToken cancellationToken)
            => (await _mediator.Send(request, cancellationToken)).ToActionResult();

        [HttpGet("Conversion/{from}/{to}")]
        public async Task<ActionResult<DonorDto>> GetDonationConversion([FromRoute] GetDonationConversionQuery request, CancellationToken cancellationToken)
            => (await _mediator.Send(request, cancellationToken)).ToActionResult();
    }
}
