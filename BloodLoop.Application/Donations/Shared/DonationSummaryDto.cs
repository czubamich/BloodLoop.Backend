using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Donations.Shared
{
    public class DonationSummaryDto
    {
        public string DonationType { get; set; }
        public int Count { get; set; }
        public int Amount { get; set; }

        public static DonationSummaryDto Null(string donationType)
            => new DonationSummaryDto() {DonationType = donationType, Count = 0, Amount = 0};
    }
}
