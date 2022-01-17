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
    sealed class DonorsWithDonationsByPeselSpec : Specification<Donor>
    {
        public DonorsWithDonationsByPeselSpec(IEnumerable<Pesel> pesels)
        {
            Query
                .Where(x => pesels.Any(p => p == x.Pesel))
                .Include(x => x.Donations);
        }
    }
}
