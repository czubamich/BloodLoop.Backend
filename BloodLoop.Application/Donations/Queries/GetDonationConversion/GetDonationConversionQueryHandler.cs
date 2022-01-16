using BloodCore.Persistance;
using BloodCore.Results;
using BloodLoop.Domain.Conversions;
using BloodLoop.Domain.Donations;
using LanguageExt;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Unit = MediatR.Unit;
using Microsoft.EntityFrameworkCore;

namespace BloodLoop.Application.Donations.Queries.GetDonationConversion
{
    public sealed class GetDonationConversionQueryHandler : IRequestHandler<GetDonationConversionQuery, Either<Error, double>>
    {

        private readonly IDonationConversionService _conversionService;
        private readonly IReadOnlyContext _context;

        public GetDonationConversionQueryHandler(IDonationConversionService conversionService, IReadOnlyContext context)
        {
            _conversionService = conversionService;
            _context = context;
        }

        public async Task<Either<Error, double>> Handle(GetDonationConversionQuery request, CancellationToken cancellationToken)
        {
            var results = await _context.GetQueryable<DonationType>()
                .Where(x => x.Label == request.FromType || x.Label == request.ToType)
                .ToListAsync();

            if (results.Count == 0 || (request.ToType == request.FromType && results.Count != 1))
                return new Error.Invalid($"Invalid parameter values for {{{nameof(request.FromType)}}} or {{{nameof(request.ToType)}}}");

            return await _conversionService.Convert(
                results.First(x => x.Label == request.FromType),
                results.First(x => x.Label == request.ToType)
            );
        }
    }
}
