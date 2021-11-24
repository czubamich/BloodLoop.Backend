using BloodLoop.Domain.Donations;
using FluentValidation;
using System.Linq;

namespace BloodLoop.Application.Donations.Queries.GetDonationInterval
{
    public class GetDonationIntervalQueryValidator : AbstractValidator<GetDonationIntervalQuery>
    {
        public GetDonationIntervalQueryValidator()
        {
            RuleFor(c => c.FromType)
                .Must(r => DonationType.GetDonationTypes().Any(dt => dt.Label == r));

            RuleFor(c => c.ToType)
                .Must(r => DonationType.GetDonationTypes().Any(dt => dt.Label == r));
        }
    }
}
