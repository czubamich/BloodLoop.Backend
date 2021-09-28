using BloodCore;
using BloodLoop.Domain.Donors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using AutoMapper;

namespace BloodLoop.Infrastructure.Persistance.Repositories
{
    [Injectable]
    public class DonorRepository : EfRepository<Donor, DonorId>, IDonorRepository
    {
        public DonorRepository(ApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator) : base(dbContext, specificationEvaluator)
        {
        }
    }
}
