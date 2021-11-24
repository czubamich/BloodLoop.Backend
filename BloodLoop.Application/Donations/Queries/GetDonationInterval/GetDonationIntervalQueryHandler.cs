using BloodCore.Persistance;
using BloodCore.Results;
using BloodLoop.Domain.Conversions;
using BloodLoop.Domain.Donations;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace BloodLoop.Application.Donations.Queries.GetDonationInterval
{
    public sealed class GetDonationIntervalQueryHandler : IRequestHandler<GetDonationIntervalQuery, Either<Error, TimeSpan>>
    {
        private readonly IDonationIntervalService _intervalService;
        private readonly IReadOnlyContext _context;

        public GetDonationIntervalQueryHandler(IDonationIntervalService intervalService, IReadOnlyContext context)
        {
            _intervalService = intervalService;
            _context = context;
        }

        public async Task<Either<Error, TimeSpan>> Handle(GetDonationIntervalQuery request, CancellationToken cancellationToken)
        {
            var results = await _context.GetQueryable<DonationType>()
                .Where(x => x.Label == request.FromType || x.Label == request.ToType)
                .ToListAsync();

            if (results.Count != 2)
                return new Error.Invalid($"Invalid parameter values for {request.FromType} or {request.ToType}");

            return await _intervalService.Convert(
                results.First(x => x.Label == request.FromType),
                results.First(x => x.Label == request.ToType)
            );
        }
    }
}
