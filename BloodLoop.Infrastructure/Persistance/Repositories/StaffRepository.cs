using BloodCore;
using Ardalis.Specification;
using AutoMapper;
using BloodLoop.Domain.Staff;

namespace BloodLoop.Infrastructure.Persistance.Repositories
{
    [Injectable]
    public class StaffRepository : EfRepository<Staff, StaffId>, IStaffRepository
    {
        public StaffRepository(ApplicationDbContext dbContext, ISpecificationEvaluator specificationEvaluator, IMapper mapper) 
            : base(dbContext, specificationEvaluator, mapper)
        {
        }
    }
}
