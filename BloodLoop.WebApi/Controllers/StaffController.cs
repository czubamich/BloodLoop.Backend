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
using BloodLoop.Application.Auth;

namespace BloodLoop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = nameof(Role.Staff) + ", " + nameof(Role.Admin))]
    public class StaffController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationContext _applicationContext;

        public StaffController(IMediator mediator, IApplicationContext applicationContext)
        {
            _mediator = mediator;
            _applicationContext = applicationContext;
        }

        [HttpPut("Donations")]
        public async Task<ActionResult<DonationDto[]>> AddDonations([FromBody] IEnumerable<DonationExtDto> donations, CancellationToken cancellationToken)
            => (await _mediator.Send(new AddDonationsCommand(_applicationContext.AccountId, donations.ToArray()), cancellationToken)).ToActionResult();

        [HttpDelete("Donations")]
        public async Task<ActionResult<DonorDto>> DeleteDonations(IEnumerable<DeleteDonationRequest> deleteDonationRequest, CancellationToken cancellationToken)
                    => (await _mediator.Send(new DeleteDonationsCommand(deleteDonationRequest), cancellationToken)).ToActionResult();

        [HttpGet("Donor")]
        public async Task<ActionResult<DonorDto>> CreateOrUpdateDonor(donorInfo, CancellationToken cancellationToken)
            => (await _mediator.Send(new GetDonorInfoQuery(_applicationContext.AccountId), cancellationToken)).ToActionResult();

        [HttpPut("Donor")]
        public async Task<ActionResult<DonorDto>> CreateOrUpdateDonor(IEnumerable<DonorDto> donorInfo, CancellationToken cancellationToken)
                    => (await _mediator.Send(new GetDonorInfoQuery(_applicationContext.AccountId), cancellationToken)).ToActionResult();
    }
}
