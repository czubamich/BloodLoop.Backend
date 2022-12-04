using AutoMapper;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Application.Staff.Shared
{
    public class DonorExtProfile : Profile
    {
        public DonorExtProfile()
        {
            CreateMap<Donor, DonorExtDto>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.Account.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Account.LastName));
        }
    }
}