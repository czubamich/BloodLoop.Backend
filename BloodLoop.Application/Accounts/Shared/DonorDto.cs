using System;
using System.Security.Policy;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Application.Accounts
{
    public class DonorDto
    {
        public DonorId Id { get; set; }

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
