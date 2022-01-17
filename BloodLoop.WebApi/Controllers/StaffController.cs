using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BloodCore.Common;
using BloodLoop.Application.Accounts;
using BloodLoop.Application.Donations;
using BloodLoop.Application.Donations.Commands.AddDonations;
using BloodLoop.Application.Services;
using BloodLoop.Domain.Accounts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using BloodLoop.Application.Staff.Commands.DeleteDonations;
using BloodLoop.Application.Staff.Shared;
using BloodLoop.Application.Staff.Queries.GetDonorInfo;
using BloodLoop.Application.Staff.Commands.VerifyDonor;

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
        public async Task<ActionResult<Unit>> AddDonations([FromBody] IEnumerable<DonationWithPeselDto> donations, CancellationToken cancellationToken)
            => (await _mediator.Send(new AddDonationsByPeselCommand(donations.ToArray()), cancellationToken)).ToActionResult();

        [HttpDelete("Donations")]
        public async Task<ActionResult<Unit>> DeleteDonations([FromBody] IEnumerable<DeleteDonationRequest> deleteDonationRequest, CancellationToken cancellationToken)
            => (await _mediator.Send(new DeleteDonationsCommand(deleteDonationRequest), cancellationToken)).ToActionResult();

        [HttpGet("Donor")]
        public async Task<ActionResult<DonorExtDto>> GetDonorInfo([FromBody] GetDonorInfoQuery getDonorInfoQuery, CancellationToken cancellationToken)
            => (await _mediator.Send(getDonorInfoQuery, cancellationToken)).ToActionResult();

        [HttpPost("Donor/Verification")]
        public async Task<ActionResult<Unit>> VerifyDonor([FromBody] VerifyDonorCommand verifyDonorCommand, CancellationToken cancellationToken)
            => (await _mediator.Send(verifyDonorCommand, cancellationToken)).ToActionResult();
    }
}
