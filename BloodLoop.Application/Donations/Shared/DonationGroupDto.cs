using System.Collections.Generic;

namespace BloodLoop.Application.Donations
{
    public class DonationGroupDto
    {
        public string Key { get;set; }
        public IEnumerable<DonationDto> Donations {  get; set; }
    }
}
