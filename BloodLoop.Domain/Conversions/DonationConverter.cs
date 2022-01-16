using BloodCore.Domain;
using BloodLoop.Domain.Donations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.DonationHelpers
{
    public class DonationConverter : ValueObject
    {
        public string DonationFromLabel { get; set; }
        public virtual DonationType DonationFrom { get; set; }
        public string DonationToLabel { get; set; }
        public virtual DonationType DonationTo { get; set; }
        public double Ratio{ get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DonationFromLabel;
            yield return DonationToLabel;
        }

        #region Constructors

        internal DonationConverter() { }
        internal DonationConverter(string donationFromLabel, string donationToLabel, double ratio) 
        {
            DonationFromLabel = donationFromLabel;
            DonationToLabel = donationToLabel;
            Ratio = ratio;
        }

        public DonationConverter(DonationType donationFrom, DonationType donationTo, double ratio)
        {
            DonationFrom = donationFrom;
            DonationFromLabel = donationFrom.Label;
            DonationTo = donationTo;
            DonationToLabel = donationTo.Label;
            Ratio = ratio;
        }

        #endregion
    }
}
