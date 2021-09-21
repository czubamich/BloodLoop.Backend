using BloodLoop.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Services
{
    public interface ICurrentAccountAccessor
    {
        AccountId AccountId { get; }
    }
}
