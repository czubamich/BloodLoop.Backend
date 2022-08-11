using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;
using Microsoft.Extensions.Logging;
using Unit = LanguageExt.Unit;

namespace BloodLoop.Application.Donations.Commands.AddDonations
{
    public class AddDonationsCommandHandler : IRequestHandler<AddDonationsCommand, Either<Error, DonationDto[]>>
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddDonationsCommandHandler> _logger;

        public AddDonationsCommandHandler(IDonorRepository donorRepository, IMapper mapper, ILogger<AddDonationsCommandHandler> logger)
        {
            _donorRepository = donorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Either<Error, DonationDto[]>> Handle(AddDonationsCommand request, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.Get(new DonorWithDonationsSpec(request.AccountId), cancellationToken);

            if (donor is null)
                return new Error.NotFound($"Donor {{{request.AccountId}}} not found");

            var newDonations = _mapper.Map<Donation[]>(request.NewDonations);

            var totalDonationCount = newDonations.Count();
            var distinctDonationCount = newDonations.Select(d =>
            {
                donor.AddDonation(d, out var isNew);
                return isNew ? 1 : 0;
            }).Sum();

            _logger.LogInformation("Added {TotalDonationCount} x Donations for {DonorId}, Distinct: {DistinctDonationCount}", totalDonationCount, donor.Id.ToString(), distinctDonationCount);

            return request.NewDonations.ToArray();
        }
    }
}