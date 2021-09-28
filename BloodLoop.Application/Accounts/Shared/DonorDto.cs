﻿using System;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Application.Accounts
{
    public class DonorDto
    {
        public Guid Id { get; set; }
        
        public GenderType Gender { get; set; }
        
        public DateTime BirthDay { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}
