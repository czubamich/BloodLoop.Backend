using System.Collections.Generic;
using AutoMapper;
using BloodLoop.Application.Donations.Shared;
using BloodLoop.Domain.Donations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace BloodLoop.Application.Donations
{
    public class DonationProfile : Profile
    {
        public DonationProfile()
        {
            CreateMap<Donation, DonationDto>()
                .ReverseMap();

            CreateMap<IGrouping<string, Donation>, DonationGroupDto>()
                .ConstructUsing((src, opt) => new DonationGroupDto() { Key = src.Key, Donations = opt.Mapper.Map<IEnumerable<DonationDto>>( src.AsEnumerable()) });

            CreateMap<DonationType, DonationTypeDto>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.DefaultName));
        }
    }
}