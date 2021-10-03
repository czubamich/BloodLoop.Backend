using FluentValidation;

namespace BloodLoop.Application.Donations.Commands.AddDonations
{
    public class AddDonationsCommandValidator : AbstractValidator<AddDonationsCommand>
    {
        public AddDonationsCommandValidator()
        {
        }
    }
}