using AutoMapper;
using BloodLoop.Domain.BloodBanks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.BloodBanks.Shared
{
    public class BloodBankProfile : Profile
    {
        public BloodBankProfile()
        {
            CreateMap<BloodBank, BloodBankDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id.Id));

            CreateMap<BloodLevel, BloodLevelDto>();
        }
    }
}
