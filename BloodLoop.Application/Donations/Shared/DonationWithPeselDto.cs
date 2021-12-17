using BloodLoop.Domain.Donors;

namespace BloodLoop.Application.Donations
{
    public class DonationWithPeselDto : DonationDto
    {
        public string Pesel { get; set; }
    }
}
