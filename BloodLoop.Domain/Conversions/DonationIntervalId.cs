using BloodCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.DonationHelpers
{
    public class DonationIntervalId : Identity<DonationIntervalId>
    {
        private DonationIntervalId(Guid id) : base(id)
        {
        }
    }
}
