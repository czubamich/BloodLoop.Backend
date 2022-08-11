using BloodCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Accounts
{
    public class AccountCreatedEvent : IDomainEvent
    {
        public AccountId AccountId { get; private set; }

        public AccountCreatedEvent(AccountId accountId) 
        { 
            AccountId = accountId; 
        }
    }
}
