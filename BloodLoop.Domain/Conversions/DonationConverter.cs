using BloodCore.Domain;
using BloodLoop.Domain.Donations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.DonationHelpers
{
    public class DonationConverter : Entity<DonationConverterId>
    {
        public DonationType DonationFrom { get; private set; }
        public DonationType DonationTo { get; private set; }
        public double Ratio { get; private set; }

        #region Constructors

        public DonationConverter(DonationType donationFrom, DonationType donationTo, double ratio)
        {
            DonationFrom = donationFrom;
            DonationTo = donationTo;
            Ratio = ratio;
        }

        #endregion
    }
}
