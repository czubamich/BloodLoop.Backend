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
        public string Label { get; private set; }
        public string Address { get; private set; }

        private List<BloodLevel> _bloodLevels;
        public virtual IReadOnlyCollection<BloodLevel> BloodLevels => _bloodLevels;

        #region Constructors

        private BloodBank()
        {
        }

        internal BloodBank(BloodBankId id, string label, string name, string address)
        {
            Id = Guard.Against.NullOrDefault(id, nameof(Id));
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Address = Guard.Against.NullOrWhiteSpace(address, nameof(address));
            Label = Guard.Against.NullOrWhiteSpace(label, nameof(label));

            _bloodLevels = new List<BloodLevel>();
        }

        #endregion

        #region Creations

        public static BloodBank Create(BloodBankId id, string label, string name, string address)
            => new(id, label, name, address);

        public static BloodBank Create(string label, string name, string address)
            => new(BloodBankId.New, label, name, address);

        #endregion

        #region Behaviours

        public void AddBloodLevel(BloodType bloodType, int measurement, DateTime measuredAt)
        {
            var bloodLevel = BloodLevel.Create(this.Id, bloodType, measurement, measuredAt);

            if(_bloodLevels is null)
                _bloodLevels = new List<BloodLevel>();

            _bloodLevels.Add(bloodLevel);

            Publish(new BloodLevelAddedEvent(bloodLevel));
        }

        #endregion
    }
}
