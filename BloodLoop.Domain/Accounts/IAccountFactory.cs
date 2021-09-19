using BloodCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Accounts
{
    public interface IAccountFactory<TAccount, TIdentity> where TAccount : AggregateRoot<TIdentity> where TIdentity : Identity
    {
        TAccount Create(AccountId id, string userName, string email, string password);
    }
}
