using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Services;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;
using Unit = MediatR.Unit;

namespace BloodLoop.Application.Donations.Commands.AddDonations
{
    public class AddDonationsByPeselCommandHandler : IRequestHandler<AddDonationsByPeselCommand, Either<Error, Unit>>
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationContext _applicationContext;

        public AddDonationsByPeselCommandHandler(IDonorRepository donorRepository, IMapper mapper)
        {
            _donorRepository = donorRepository;
            _mapper = mapper;
        }

        public async Task<Either<Error, Unit>> Handle(AddDonationsByPeselCommand request, CancellationToken cancellationToken)
        {
            var validPesels = request.NewDonations.Select(x => new Pesel(x.Pesel)).Distinct();

            var donors = await _donorRepository.Find(new DonorsWithDonationsByPeselSpec(validPesels), cancellationToken);

            var donorsDonations = request.NewDonations.GroupBy(x => x.Pesel)
                .Select(x =>
                new
                {
                    Donor = donors.FirstOrDefault(d => d.Pesel.Value == x.Key),
                    Donations = _mapper.Map<Donation[]>(request.NewDonations.Where(d => d.Pesel == x.Key))
                });

            var bloodBankId = _applicationContext.BloodBank;

            donorsDonations.ToList().ForEach(dd =>
                dd.Donations.ToList().ForEach(d => dd.Donor.AddDonation(d.ChangeBloodBank(bloodBankId))));

            return Unit.Value;
        }
    }
}