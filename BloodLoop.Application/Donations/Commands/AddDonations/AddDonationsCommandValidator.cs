using FluentValidation;

namespace BloodLoop.Application.Donations.Commands.AddDonations
{
    public class AddDonationsCommandValidator : AbstractValidator<AddDonationsByPeselCommand>
    {
        public AddDonationsCommandValidator()
        {
        }
    }
}