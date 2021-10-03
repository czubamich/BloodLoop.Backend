using BloodCore.Results;
using BloodLoop.Application.Donations.Shared;
using BloodLoop.Domain.Accounts;
using LanguageExt;
using MediatR;

namespace BloodLoop.Application.Donations.Queries.GetDonationsSummary
{
    public class GetDonationsSummaryQuery : IRequest<Either<Error, DonationSummaryDto>>
    {
        public AccountId AccountId { get; set; }
        public string DonationTypeLabel { get; set; }

        public GetDonationsSummaryQuery(AccountId accountId, string donationTypeLabel)
        {
            AccountId = accountId;
            DonationTypeLabel = donationTypeLabel;
        }
    }
}