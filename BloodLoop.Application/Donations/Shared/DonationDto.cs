using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using BloodLoop.Domain.Donations;

namespace BloodLoop.Application.Donations
{
    public class DonationDto
    {
        DonationId Id { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public int Volume { get; set; }

        public string DonationTypeLabel { get; set; }
    }
}
