using BloodCore.Results;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Application.Staff.Shared;
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

namespace BloodLoop.Application.Staff.Queries.GetDonorInfo
{
    public class GetDonorInfoQueryHandler : IRequestHandler<GetDonorInfoQuery, Either<Error, DonorExtDto>>
    {
        private readonly IDonorRepository _donorRepository;

        public GetDonorInfoQueryHandler(IDonorRepository donorRepository)
        {
            _donorRepository = donorRepository;
        }

        public async Task<Either<Error, DonorExtDto>> Handle(GetDonorInfoQuery request, CancellationToken cancellationToken)
        {
            DonorExtDto donor;

            donor = await _donorRepository.GetAs<DonorExtDto>(new DonorByEmailSpec(request.EmailOrPesel), cancellationToken);

            if (donor is null && Pesel.TryParse(request.EmailOrPesel, out Pesel pesel))
                donor = await _donorRepository.GetAs<DonorExtDto>(new DonorByPeselSpec(pesel), cancellationToken);

            if (donor is null)
                return new Error.NotFound($"User {request.EmailOrPesel} was not found");

            return donor;
        }
    }
}
