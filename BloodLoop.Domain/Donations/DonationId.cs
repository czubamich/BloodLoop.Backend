using BloodCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Donations
{
    public class DonationId : Identity<DonationId>
    {
        private DonationId(Guid id) : base(id) { }
    }
}
