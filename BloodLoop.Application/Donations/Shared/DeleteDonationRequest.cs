using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Staff.Commands.DeleteDonations
{
    public class DeleteDonationRequest
    {
        public string Pesel { get; set; }
        public DateTime Date { get; set; }
    }
}
