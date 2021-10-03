using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BloodCore.Results;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = LanguageExt.Unit;

namespace BloodLoop.Application.Donations.Queries.GetDonations
{
    public class GetDonationsQueryHandler : IRequestHandler<GetDonationsQuery, Either<Error, IEnumerable<DonationDto>>>
    {
        public readonly IDonorRepository _donorRepository;
        public readonly IMapper _mapper;

        public GetDonationsQueryHandler(IDonorRepository donorRepository, IMapper mapper)
        {
            _donorRepository = donorRepository;
            _mapper = mapper;
        }


        public async Task<Either<Error, IEnumerable<DonationDto>>> Handle(GetDonationsQuery request, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.Get(new DonorWithDonationsSpec(request.AccountId), cancellationToken);

            if (donor is null)
                return new Error.NotFound($"Donor {{{request.AccountId}}} not found");

            return _mapper.Map<DonationDto[]>(donor.Donations);
        }
    }
}