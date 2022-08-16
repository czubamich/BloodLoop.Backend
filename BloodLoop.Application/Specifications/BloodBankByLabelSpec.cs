using Ardalis.Specification;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Specifications
{
    public sealed class BloodBankByLabelSpec : Specification<BloodBank>
    {
        public BloodBankByLabelSpec(string label)
        {
            Query
                .Where(x => x.Label == label)
                .Include(x => x.BloodLevels);
        }
    }
}
