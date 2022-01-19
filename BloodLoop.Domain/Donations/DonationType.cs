using BloodCore.Domain;
using System.Collections.Generic;

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

        public static DonationType Of(string label)
        {
            return new DonationType(label, "", 0);
        }

        #endregion

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Label;
        }

        #region Defaults

        public static DonationType Whole => new(nameof(Whole).ToLower(), "Whole Blood", 450);
        public static DonationType Plasma => new(nameof(Plasma).ToLower(), "Plasma", 600);
        public static DonationType Platelets => new(nameof(Platelets).ToLower(), "Platelets", 500);
        public static DonationType RedCells => new(nameof(RedCells).ToLower(), "Red Cells", 500);
        public static DonationType Disqualified => new(nameof(Disqualified).ToLower(), "Disqualified", 0);

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