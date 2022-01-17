using BloodCore;
using BloodLoop.Domain.Donors;
using Ardalis.Specification;
using AutoMapper;
using BloodLoop.Domain.BloodBanks;

namespace BloodLoop.Infrastructure.Persistance.Repositories
{
    [Injectable]
    public class DonorRepository : EfRepository<Donor, DonorId>, IDonorRepository
    {
        public DonorRepository(ApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator, IMapper mapper) 
            : base(dbContext, specificationEvaluator, mapper)
        {
        }
    }
}
