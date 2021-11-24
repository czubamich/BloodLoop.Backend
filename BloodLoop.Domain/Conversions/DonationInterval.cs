using BloodCore.Domain;
using BloodLoop.Domain.Donations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.DonationHelpers
{
    public class DonationInterval : Entity<DonationIntervalId>
    {
        public virtual DonationType DonationFrom { get; set; }
        public virtual DonationType DonationTo { get; set; }
        public TimeSpan Interval { get; set; }

        #region Constructors

        internal DonationInterval(DonationType donationFrom, DonationType donationTo, TimeSpan interval)
        {
            DonationFrom = donationFrom;
            DonationTo = donationTo;
            Interval = interval;
        }

        #endregion
    }
}
