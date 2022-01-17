using BloodLoop.Domain.Donors;
using FluentValidation;

namespace BloodLoop.Application.Donations.Commands.AddDonations
{
    public class AddDonationsByPeselCommandValidator : AbstractValidator<AddDonationsByPeselCommand>
    {
        public AddDonationsByPeselCommandValidator()
        {
            RuleForEach(x => x.NewDonations)
                .Must(p => Pesel.TryParse(p.Pesel, out _));
        }
    }
}