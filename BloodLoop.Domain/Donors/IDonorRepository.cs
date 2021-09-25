using BloodCore.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Donors
{
    public interface IDonorRepository : IRepository<Donor, DonorId>
    {
    }
}
