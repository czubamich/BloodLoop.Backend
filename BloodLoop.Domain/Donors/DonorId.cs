using BloodCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Donors
{
    public class DonorId : Identity<DonorId>
    {
        private DonorId(Guid id) : base(id)
        {
        }
    }
}
