using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetDonorInfoQueryHandler(IDonorRepository donorRepository, IMapper mapper)
        {
            _donorRepository = donorRepository;
            _mapper = mapper;
        }

        public async Task<Either<Error, DonorExtDto>> Handle(GetDonorInfoQuery request, CancellationToken cancellationToken)
        {
            Donor donor;

            if(request.DonorId is not null)
                donor = await _donorRepository.Get(new DonorByAccountIdSpec(request.DonorId.AsAccountId), cancellationToken);
            else if (request.EmailOrPesel is not null)
                donor = await _donorRepository.Get(new DonorByEmailSpec(request.EmailOrPesel), cancellationToken);
            else
                return new Error.Invalid("DonorId or Email missing");


            if (donor is null && Pesel.TryParse(request.EmailOrPesel, out Pesel pesel))
                donor = await _donorRepository.Get(new DonorByPeselSpec(pesel), cancellationToken);

            if (donor is null)
                return new Error.NotFound($"User {request.EmailOrPesel} was not found");

            return _mapper.Map<DonorExtDto>(donor);
        }
    }
}
