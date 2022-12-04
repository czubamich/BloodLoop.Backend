using AutoMapper;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Application.Accounts.Shared
{
    public class DonorProfile : Profile
    {
        public DonorProfile()
        {
            CreateMap<Donor, DonorDto>()
                .ForMember(dst => dst.FirstName, opt => opt.MapFrom(src => src.Account.FirstName))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Account.LastName));

            CreateMap<Pesel, string>()
                .ConstructUsing(src => src.Value);

            CreateMap<string, Pesel>()
                .ConstructUsing(src => new Pesel(src));

            CreateMap<BloodType, DonorDto.BloodTypeDto>();
        }
    }
}