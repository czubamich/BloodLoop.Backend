using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Services;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodLoop.Application.Accounts.Queries
{
    class GetCurrentDonorInfoQueryHandler : IRequestHandler<GetCurrentDonorInfoQuery, Either<Error, DonorDto>>
    {
        private readonly IApplicationContext _appContext;
        private readonly IDonorRepository _donorRepository;

        public GetCurrentDonorInfoQueryHandler(IDonorRepository donorRepository, IApplicationContext appContext)
        {
            _donorRepository = donorRepository;
            _appContext = appContext;
        }

        public async Task<Either<Error, DonorDto>> Handle(GetCurrentDonorInfoQuery request, CancellationToken cancellationToken)
        {
            var accountId = _appContext.AccountId;
            var donorDto = await _donorRepository.GetAs<DonorDto>(new GetDonorByAccountIdSpec(accountId), cancellationToken);

            if (donorDto is null)
                return new Error.NotFound("Donor not found");

            return donorDto;
        }
    }
}
