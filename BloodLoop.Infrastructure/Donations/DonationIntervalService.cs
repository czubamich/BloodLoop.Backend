using BloodCore;
using BloodCore.Persistance;
using BloodLoop.Domain.Conversions;
using BloodLoop.Domain.DonationHelpers;
using BloodLoop.Domain.Donations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Infrastructure.Donations
{
    [Injectable]
    internal class DonationIntervalService : IDonationIntervalService
    {
        private readonly IReadOnlyContext _context;

        public DonationIntervalService(IReadOnlyContext context)
        {
            _context = context;
        }

        public Task<TimeSpan> Convert(DonationType fromType, DonationType toType)
        {
            var interval = _context.GetQueryable<DonationInterval>()
                .Where(x => x.DonationFrom.Label == fromType.Label && x.DonationTo.Label == toType.Label)
                .Select(x => x.Interval)
                .FirstOrDefaultAsync();

            return interval;
        }
    }
}
