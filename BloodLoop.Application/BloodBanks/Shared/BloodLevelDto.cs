using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BloodLoop.Application.Accounts.DonorDto;

namespace BloodLoop.Application.BloodBanks.Shared
{
    public class BloodLevelDto
    {
        public DateTime CreatedAt { get; set; }
        public BloodTypeDto BloodType { get; set; }
        public int Measurement { get; set; }
    }
}
