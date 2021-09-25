using Ardalis.GuardClauses;
using BloodCore.Common;
using BloodLoop.Domain.DonationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Donations
{
    public class DonationType : Entity<DonationTypeId>
    {
        public string Label { get; private set; }
        public string DefaultName { get; private set; }
        public int DefaultVolume { get; private set; }

        #region Constructors

        private DonationType() {}

        internal DonationType(DonationTypeId id, string label, string defaultName, int defaultVolume) : base(id)
        {
            Label = Guard.Against.InvalidFormat(label, nameof(Label), "^[a-z0-9_\\-]+$");
            DefaultName = Guard.Against.NullOrEmpty(defaultName, nameof(DefaultName));
            DefaultVolume = Guard.Against.Negative(defaultVolume, nameof(DefaultVolume));
        }

        #endregion

        #region Creations

        internal static DonationType Create(DonationTypeId id, string label, string defaultName, int defaultVolume)
            => new DonationType(id, label, defaultName, defaultVolume);

        internal static DonationType Create(string label, string defaultName, int defaultVolume)
            => new DonationType(DonationTypeId.New, label, defaultName, defaultVolume);

        #endregion

        #region Defaults

        public static DonationType Whole        => Create(DonationTypeId.Of("d03ebd31-7490-4c6e-ae55-135024b9f500"), nameof(Whole).ToLower(), "Whole Blood", 450);
        public static DonationType Plasma       => Create(DonationTypeId.Of("9466bb55-e230-471d-afa5-3ea22801bdb8"), nameof(Plasma).ToLower(), "Plasma", 600);
        public static DonationType Platelets    => Create(DonationTypeId.Of("d03ebd31-7490-4c6e-ae55-135024b9f500"), nameof(Platelets).ToLower(), "Platelets", 500);
        public static DonationType RedCells     => Create(DonationTypeId.Of("d03ebd31-7490-4c6e-ae55-135024b9f500"), nameof(RedCells).ToLower(), "Red Cells", 500);
        public static DonationType Disqualified => Create(DonationTypeId.Of("d03ebd31-7490-4c6e-ae55-135024b9f500"), nameof(Disqualified).ToLower(), "Disqualified", 0);

        #endregion
    }
}
