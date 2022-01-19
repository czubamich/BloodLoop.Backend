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
using BloodLoop.Application.BloodBanks.Shared;
using BloodLoop.Application.BloodBanks.Commands;
using BloodLoop.Application.BloodBanks.Queries;
using BloodLoop.Application.Staff.Shared;
using BloodLoop.Application.Staff.Commands;

namespace BloodLoop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(nameof(Role.Admin))]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationContext _applicationContext;

        public AdminController(IMediator mediator, IApplicationContext applicationContext)
        {
            _mediator = mediator;
            _applicationContext = applicationContext;
        }

        [HttpPost("Staff")]
        public async Task<ActionResult<Unit>> RegisterStaff([FromBody] RegisterStaffCommand registerStaffCommand, CancellationToken cancellationToken)
            => (await _mediator.Send(registerStaffCommand, cancellationToken)).ToActionResult();

        [HttpPost("BloodBanks")]
        public async Task<ActionResult<BloodBankDto>> RegisterBloodBank([FromBody] RegisterBloodBankCommand registerBloodBankCommand, CancellationToken cancellationToken)
            => (await _mediator.Send(registerBloodBankCommand, cancellationToken)).ToActionResult();

        [HttpGet("BloodBanks")]
        public async Task<ActionResult<BloodBankDto[]>> GetBloodBank(CancellationToken cancellationToken)
            => (await _mediator.Send(new GetBloodBanksQuery(), cancellationToken)).ToActionResult();
    }
}
