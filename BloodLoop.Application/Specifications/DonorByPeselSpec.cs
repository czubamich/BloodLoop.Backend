using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using AutoMapper;
using BloodLoop.Application.Accounts;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Application.Specifications.Accounts
{
    sealed class DonorByPeselSpec : Specification<Donor>
    {
        public DonorByPeselSpec(Pesel pesel)
        {
            Query
                .Where(x => x.Pesel == pesel);
        }
    }
}
