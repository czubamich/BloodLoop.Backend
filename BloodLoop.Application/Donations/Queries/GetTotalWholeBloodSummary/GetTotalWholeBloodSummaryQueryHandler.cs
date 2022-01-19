using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Donations.Shared;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Domain.Conversions;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;

namespace BloodLoop.Application.Donations.Queries.GetDonationsSummary
{
    public class GetTotalWholeBloodSummaryQueryHandler : IRequestHandler<GetTotalWholeBloodSummaryQuery, Either<Error, DonationSummaryDto>>
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IDonationConversionService _conversionService;
        private readonly IMapper _mapper;

        public GetTotalWholeBloodSummaryQueryHandler(IDonorRepository donorRepository, IMapper mapper, IDonationConversionService conversionService)
        {
            _donorRepository = donorRepository;
            _mapper = mapper;
            _conversionService = conversionService;
        }

        public async Task<Either<Error, DonationSummaryDto>> Handle(GetTotalWholeBloodSummaryQuery request, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.Get(new DonorWithDonationsSpec(request.AccountId), cancellationToken);

            if (donor is null)
                return new Error.NotFound($"Donor {{{request.AccountId}}} not found");

            var donations = donor.Donations.ToList();

            var convertedDonationsTasks = donations.GroupBy(x => x.DonationTypeLabel)
                .Select((group) => group.Sum(x => x.Volume)*(_conversionService.Convert(DonationType.Of(group.Key), DonationType.Whole).Result))
                .ToList();

            var donationsTotalVolume = convertedDonationsTasks.Sum();

            return new DonationSummaryDto()
            {
                DonationType = "total",
                Count = donations.Count,
                Amount = (int)(donationsTotalVolume + 0.5f)
            };
        }
    }
}