using BloodCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.DonationHelpers
{
    public class DonationConverterId : Identity<DonationConverterId>
    {
        private DonationConverterId(Guid id) : base(id)
        {
        }
    }
}
