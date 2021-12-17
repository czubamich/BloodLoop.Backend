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
    public class AddDonationsByPeselCommandHandler : IRequestHandler<AddDonationsByPeselCommand, Either<Error, AddDonationsByPeselResponse>>
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IMapper _mapper;

        public AddDonationsByPeselCommandHandler(IDonorRepository donorRepository, IMapper mapper)
        {
            _donorRepository = donorRepository;
            _mapper = mapper;
        }

        public async Task<Either<Error, AddDonationsByPeselResponse>> Handle(AddDonationsByPeselCommand request, CancellationToken cancellationToken)
        {
            var validPesels = request.NewDonations.Select(x => new Pesel(x.Pesel));

            var donors = await _donorRepository.Find(new DonorsWithDonationsByPeselSpec(validPesels), cancellationToken);

            var donorsDonations = request.NewDonations.GroupBy(x => x.Pesel)
                .Select(x =>
                new
                {
                    Donor = donors.FirstOrDefault(d => d.Pesel.Value == x.Key),
                    Donations = _mapper.Map<Donation[]>(request.NewDonations.Where(d => d.Pesel == x.Key))
                });

            donorsDonations.ToList().ForEach(dd =>
                dd.Donations.ToList().ForEach(d => dd.Donor.AddDonation(d)));
        }
    }
}