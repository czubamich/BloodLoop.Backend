using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Donations.Shared;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = LanguageExt.Unit;

namespace BloodLoop.Application.Donations.Queries.GetDonationsSummary
{
    public class GetDonationsSummaryQueryHandler : IRequestHandler<GetDonationsSummaryQuery, Either<Error, DonationSummaryDto>>
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IMapper _mapper;

        public GetDonationsSummaryQueryHandler(IDonorRepository donorRepository, IMapper mapper)
        {
            _donorRepository = donorRepository;
            _mapper = mapper;
        }

        public async Task<Either<Error, DonationSummaryDto>> Handle(GetDonationsSummaryQuery request, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.Get(new DonorWithDonationsSpec(request.AccountId), cancellationToken);

            if (donor is null)
                return new Error.NotFound($"Donor {{{request.AccountId}}} not found");

            var donationSummary = donor.Donations
                .Where(x => x.DonationTypeLabel.Equals(request.DonationTypeLabel,StringComparison.InvariantCultureIgnoreCase))
                .GroupBy(x => x.DonationTypeLabel)
                .Select(x => new DonationSummaryDto()
                {
                    DonationType = x.Key,
                    Count = x.Count(),
                    Amount = x.Sum(d => d.Volume)
                })
                .FirstOrDefault();

            if (donationSummary is null)
                return DonationSummaryDto.Null(request.DonationTypeLabel);

            return donationSummary;
        }
    }
}