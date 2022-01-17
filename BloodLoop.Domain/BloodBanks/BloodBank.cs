using Ardalis.GuardClauses;
using BloodCore.Domain;
using BloodCore.Extensions;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace BloodLoop.Domain.BloodBanks
{
    public class BloodBank : AggregateRoot<BloodBankId>
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        #region Constructors

        private BloodBank()
        {
        }

        internal BloodBank(BloodBankId id, string name, string address)
        {
            Id = Guard.Against.NullOrDefault(id, nameof(Id));
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Address = Guard.Against.NullOrWhiteSpace(address, nameof(address));
        }

        #endregion

        #region Creations

        public static BloodBank Create(BloodBankId id, string name, string address)
            => new(id, name, address);

        public static BloodBank Create(string name, string address)
            => new(BloodBankId.New, name, address);

        #endregion

        #region Behaviours
        #endregion
    }
}
