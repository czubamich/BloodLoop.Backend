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
        private readonly ICurrentAccountAccessor _currentAccountAccessor;
        private readonly IDonorRepository _donorRepository;

        public GetCurrentDonorInfoQueryHandler(IDonorRepository donorRepository, ICurrentAccountAccessor currentAccountAccessor)
        {
            _donorRepository = donorRepository;
            _currentAccountAccessor = currentAccountAccessor;
        }

        public async Task<Either<Error, DonorDto>> Handle(GetCurrentDonorInfoQuery request, CancellationToken cancellationToken)
        {
            var accountId = _currentAccountAccessor.AccountId;
            var donorDto = (DonorDto)null;// await _donorRepository.Find<DonorDto>(new GetDonorByAccountIdSpec(accountId), cancellationToken);

            if (donorDto is null)
                return new Error.NotFound("Donor not found");

            return donorDto;
        }
    }
}
