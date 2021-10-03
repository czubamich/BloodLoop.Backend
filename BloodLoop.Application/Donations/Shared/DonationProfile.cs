using System;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using BloodLoop.Application.Donations.Shared;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;

namespace BloodLoop.Application.Donations
{
    public class DonationProfile : Profile
    {
        public DonationProfile()
        {
            CreateMap<Donation, DonationDto>()
                .ReverseMap();

            CreateMap<DonationType, DonationTypeDto>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.DefaultName));
        }
    }
}