using BloodCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Donors
{
    class Donor : Entity<DonorId>
    {
        public Donor() {}

        public Donor(DonorId id) : base(id)
        {
        }
    }
}
