using BloodLoop.Domain.Donors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Staff.Shared
{
    public class DonorExtDto
    {
        public Guid Id { get; set; }

        public string Pesel { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public GenderType Gender { get; set; }

        public BloodTypeDto BloodType { get; set; }

        public class BloodTypeDto
        {
            public string Label { get; set; }
            public string Symbol { get; set; }
        }
    }
}
