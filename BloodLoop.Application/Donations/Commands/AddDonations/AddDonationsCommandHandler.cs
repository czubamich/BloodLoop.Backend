using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace BloodLoop.Application.Donations.Commands.AddDonations
{
    public class AddDonationsCommandHandler : IRequestHandler<AddDonationsCommand, Either<Error, DonationDto[]>>
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IMapper _mapper;

        public AddDonationsCommandHandler(IDonorRepository donorRepository, IMapper mapper)
        {
            _donorRepository = donorRepository;
            _mapper = mapper;
        }

        public async Task<Either<Error, DonationDto[]>> Handle(AddDonationsCommand request, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.Get(new DonorWithDonationsSpec(request.AccountId), cancellationToken);

            if (donor is null)
                return new Error.NotFound($"Donor {{{request.AccountId}}} not found");

            var newDonations = _mapper.Map<Donation[]>(request.NewDonations);

            foreach(var donation in newDonations)
                donor.AddDonation(donation);

            return request.NewDonations.ToArray();
        }
    }
}