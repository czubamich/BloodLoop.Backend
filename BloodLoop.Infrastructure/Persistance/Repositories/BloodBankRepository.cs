using BloodCore;
using BloodLoop.Domain.Donors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using AutoMapper;
using BloodLoop.Domain.BloodBanks;

namespace BloodLoop.Infrastructure.Persistance.Repositories
{
    [Injectable]
    public class BloodBankRepository : EfRepository<BloodBank, BloodBankId>, IBloodBankRepository
    {
        public BloodBankRepository(ApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator, IMapper mapper) 
            : base(dbContext, specificationEvaluator, mapper)
        {
        }
    }
}
