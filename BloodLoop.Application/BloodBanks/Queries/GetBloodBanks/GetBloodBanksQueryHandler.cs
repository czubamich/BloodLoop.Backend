using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Persistance;
using BloodCore.Results;
using BloodLoop.Application.BloodBanks.Shared;
using BloodLoop.Application.Specifications;
using BloodLoop.Domain.BloodBanks;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BloodLoop.Application.BloodBanks.Queries
{
    public class GetBloodBanksQueryHandler : IRequestHandler<GetBloodBanksQuery, Either<Error, BloodBankDto[]>>
    {
        private readonly IReadOnlyContext _readOnlyContext;
        private readonly IMapper _mapper;

        public GetBloodBanksQueryHandler(IMapper mapper, IReadOnlyContext readOnlyContext)
        {
            _mapper = mapper;
            _readOnlyContext = readOnlyContext;
        }

        public async Task<Either<Error, BloodBankDto[]>> Handle(GetBloodBanksQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<BloodBankDto[]>(await _readOnlyContext.GetQueryable<BloodBank>().ToArrayAsync());
        }
    }
}