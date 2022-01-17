using BloodCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.BloodBanks
{
    public class BloodBankId : Identity<BloodBankId>
    {
        private BloodBankId(Guid id) : base(id)
        {
        }
    }
}
