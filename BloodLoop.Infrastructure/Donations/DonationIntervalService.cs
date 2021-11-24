using BloodCore;
using BloodCore.Persistance;
using BloodLoop.Domain.Conversions;
using BloodLoop.Domain.DonationHelpers;
using BloodLoop.Domain.Donations;
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

        public double Convert(DonationType fromType, DonationType toType)
        {
            _context.GetQueryable<DonationInterval>();
        }
    }
}
