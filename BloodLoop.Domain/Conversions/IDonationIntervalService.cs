using BloodLoop.Domain.Donations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Conversions
{
    public interface IDonationIntervalService
    {
        Task<TimeSpan> Convert(DonationType fromType, DonationType toType);
    }
}
