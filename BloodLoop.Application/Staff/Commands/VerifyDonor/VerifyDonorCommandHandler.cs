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

namespace BloodLoop.Application.Staff.Commands.VerifyDonor
{
    public class VerifyDonorCommandHandler : IRequestHandler<VerifyDonorCommand, Either<Error, Unit>>
    {
        private readonly IDonorRepository _donorRepository;

        public VerifyDonorCommandHandler(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public async Task<Either<Error, Unit>> Handle(VerifyDonorCommand request, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.Get(new DonorByEmailSpec(request.Email), cancellationToken);

            if(donor is null)
                return new Error.NotFound($"User {request.Email} does not exist!");

            donor.SetPesel(new Pesel(request.Pesel));

            return Unit.Value;
        }
    }
}
