using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using static BloodLoop.Application.Accounts.DonorDto;
using BloodLoop.Domain.Donations;
using AutoMapper;

namespace BloodLoop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DictionariesController : ControllerBase
    {
        private readonly IMapper mapper;

        public DictionariesController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("BloodTypes")]
        public ActionResult<BloodTypeDto[]> GetBloodTypes()
            => Ok(mapper.Map<BloodTypeDto[]>(BloodType.GetBloodTypes()));
    }
}
