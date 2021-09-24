using Ardalis.GuardClauses;
using BloodCore.Common;
using BloodLoop.Domain.DonationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Donation
{
    public class DonationType : AggregateRoot<DonationTypeId>
    {
        public string Label { get; private set; }
        public string DefaultName { get; private set; }
        public int DefaultVolume { get; private set; }

        #region Creations

        internal DonationType(DonationTypeId id, string label, string defaultName, int defaultVolume) : base(id)
        {
            Label = Guard.Against.InvalidFormat(label, nameof(Label), "^[a-z0-9_\\-]+$");
            DefaultName = Guard.Against.NullOrEmpty(defaultName, nameof(DefaultName));
            DefaultVolume = Guard.Against.NegativeOrZero(defaultVolume, nameof(DefaultVolume));
        }

        internal DonationType Create(DonationTypeId id, string label, string defaultName, int defaultVolume)
            => new DonationType(id, label, defaultName, defaultVolume);

        internal DonationType Create(string label, string defaultName, int defaultVolume)
            => new DonationType(DonationTypeId.New, label, defaultName, defaultVolume);

        #endregion
    }
}
