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
    internal class DonationConversionService : IDonationConversionService
    {
        private readonly IReadOnlyContext _context;

        public DonationConversionService(IReadOnlyContext context)
        {
            _context = context;
        }

        public Task<double> Convert(DonationType fromType, DonationType toType)
        {
            var ration = _context.GetQueryable<DonationConverter>()
                .Where(x => x.DonationFrom.Label == fromType.Label && x.DonationTo.Label == toType.Label)
                .Select(x => x.Ratio)
                .FirstOrDefaultAsync();

            return ration;
        }
    }
}
