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
    public sealed class GetDonationIntervalForUserQueryHandler : IRequestHandler<GetDonationIntervalForUserQuery, Either<Error, TimeSpan>>
    {
        private readonly IDonationIntervalService _intervalService;
        private readonly IReadOnlyContext _context;

        public GetDonationIntervalForUserQueryHandler(IDonationIntervalService intervalService, IReadOnlyContext context)
        {
            _intervalService = intervalService;
            _context = context;
        }

        public async Task<Either<Error, TimeSpan>> Handle(GetDonationIntervalForUserQuery request, CancellationToken cancellationToken)
        {
            var latestDonation = await _context.GetQueryable<Donation>()
                .Where(x => x.DonorId.Id == request.AccountId.Id)
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync();

            if (latestDonation is null)
                return TimeSpan.Zero;

            var donationTypes = await _context.GetQueryable<DonationType>()
                .Where(x => x.Label == latestDonation.DonationTypeLabel || x.Label == request.ToType)
                .ToListAsync();

            if (!donationTypes.Any(x => x.Label == request.ToType))
                return new Error.Invalid($"Invalid parameter values for {{{nameof(request.ToType)}}}");

            var requiredInterval = await _intervalService.Convert(
                donationTypes.First(x => x.Label == latestDonation.DonationTypeLabel),
                donationTypes.First(x => x.Label == request.ToType)
            );

            var nextDonationDate = (latestDonation.Date + requiredInterval);

            return nextDonationDate > DateTime.UtcNow ? DateTime.UtcNow - nextDonationDate : TimeSpan.Zero;
        }
    }
}
