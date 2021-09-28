using AutoMapper;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Application.Accounts.Shared
{
    public class DonorProfile : Profile
    {
        public DonorProfile()
        {
            CreateMap<Donor, DonorDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Id))
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.Account.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Account.LastName));
        }
    }
}