using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace BloodLoop.Application.Staff.Commands.DeleteDonations
{
    internal class DeleteDonationsCommandHandler : IRequestHandler<DeleteDonationsCommand, Either<Error, Unit>>
    {
        private readonly IDonorRepository _donorRepository;

        public DeleteDonationsCommandHandler(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public async Task<Either<Error, Unit>> Handle(DeleteDonationsCommand request, CancellationToken cancellationToken)
        {
            var validPesels = request.DonationsToDelete.Select(x => new Pesel(x.Pesel)).Distinct();

            var donors = await _donorRepository.Find(new DonorsWithDonationsByPeselSpec(validPesels), cancellationToken);

            var donorsDonations = request.DonationsToDelete.GroupBy(x => x.Pesel)
                .Select(x => new {
                    Donor = donors.First(d => d.Pesel.Value == x.Key),
                    Dates = x.Select(x => x.Date)
                });
            
            donorsDonations.ToList().ForEach(dd =>
                dd.Dates.ToList().ForEach(d => dd.Donor.RemoveDonation(d)));

            return Unit.Value;
        }
    }
}
