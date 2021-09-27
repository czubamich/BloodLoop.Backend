using System;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using BloodLoop.Domain.Donors;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;

namespace BloodLoop.Application.Shared
{
    public class DonorProfile : Profile
    {
        public DonorProfile()
        {
            CreateMap<Donor, DonorDto>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id.Id));
        }
    }
}