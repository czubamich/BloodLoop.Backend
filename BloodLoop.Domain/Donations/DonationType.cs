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
    public class DonationType : ValueObject
    {
        public string Label { get; private set; }
        public string DefaultName { get; private set; }
        public int DefaultVolume { get; private set; }

        #region Constructors

        private DonationType() {}

        internal DonationType(string label, string defaultName, int defaultVolume)
        {
            Label = label;
            DefaultName = defaultName;
            DefaultVolume = defaultVolume;
        }

        #endregion

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }

        #region Defaults

        public static DonationType Whole        => new DonationType(nameof(Whole).ToLower(), "Whole Blood", 450);
        public static DonationType Plasma       => new DonationType(nameof(Plasma).ToLower(), "Plasma", 600);
        public static DonationType Platelets    => new DonationType(nameof(Platelets).ToLower(), "Platelets", 500);
        public static DonationType RedCells     => new DonationType(nameof(RedCells).ToLower(), "Red Cells", 500);
        public static DonationType Disqualified => new DonationType(nameof(Disqualified).ToLower(), "Disqualified", 0);

        public static DonationType[] GetDonationTypes()
        {
            return new[]
            {
                Whole, Plasma, Platelets, RedCells, Disqualified
            };
        }

        #endregion
    }
}
