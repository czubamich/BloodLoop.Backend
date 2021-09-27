using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Application.Shared
{
    public class DonorDto
    {
        public Guid Id { get; set; }

        public Pesel Pesel { get; private set; }
        
        public GenderType Gender { get; private set; }
        
        public DateTime BirthDay { get; private set; }

        public string FirstName { get; private set; }
        
        public string LastName { get; private set; }
    }
}
