using BloodLoop.Domain.Donations;
using FluentValidation;
using System.Linq;

namespace BloodLoop.Application.Donations.Queries.GetDonationInterval
{
    public class GetDonationIntervalForUserQueryValidator : AbstractValidator<GetDonationIntervalForUserQuery>
    {
        public GetDonationIntervalForUserQueryValidator()
        {
            RuleFor(c => c.ToType)
                .Must(r => DonationType.GetDonationTypes().Any(dt => dt.Label == r));
        }
    }
}
