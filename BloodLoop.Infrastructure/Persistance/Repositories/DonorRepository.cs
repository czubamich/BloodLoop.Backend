using BloodLoop.Domain.Donors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Infrastructure.Persistance.Repositories
{
    public class DonorRepository : EfRepository<Donor, DonorId>, IDonorRepository
    {
        public DonorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
