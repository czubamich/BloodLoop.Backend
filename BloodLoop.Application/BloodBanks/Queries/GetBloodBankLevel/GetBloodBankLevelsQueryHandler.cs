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
using Serilog;

namespace BloodLoop.Application.BloodBanks.Queries
{
    public class GetBloodBankLevelsQueryHandler : IRequestHandler<GetBloodBankLevelsQuery, Either<Error, BloodLevelDto[]>>
    {
        private readonly IReadOnlyContext _readOnlyContext;
        private readonly IMapper _mapper;

        public GetBloodBankLevelsQueryHandler(IMapper mapper, IReadOnlyContext readOnlyContext)
        {
            _mapper = mapper;
            _readOnlyContext = readOnlyContext;
        }

        public async Task<Either<Error, BloodLevelDto[]>> Handle(GetBloodBankLevelsQuery request, CancellationToken cancellationToken)
        {
            var bloodLevels = await _readOnlyContext.GetQueryable<BloodLevel>().OrderByDescending(x => x.CreatedAt).Take(16).ToArrayAsync();
            var newest = bloodLevels.FirstOrDefault();

            if(newest is null)
                return new BloodLevelDto[0];

            var currentBloodLevels = bloodLevels.Where(x => x.CreatedAt == newest.CreatedAt);

            Log.Information("Context test");

            return _mapper.Map<BloodLevelDto[]>(currentBloodLevels.OrderBy(x => x.BloodType.Label));
        }
    }
}