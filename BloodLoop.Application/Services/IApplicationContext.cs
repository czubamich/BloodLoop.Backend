using BloodLoop.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodLoop.Domain.BloodBanks;

namespace BloodLoop.Application.Services
{
    public interface IApplicationContext
    {
        AccountId AccountId { get; }
        BloodBankId BloodBank { get; }
        IEnumerable<Role> Roles { get; }
    }
}
