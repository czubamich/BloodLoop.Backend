using BloodCore.Results;
using BloodLoop.Application.Donations.Shared;
using BloodLoop.Domain.Accounts;
using LanguageExt;
using MediatR;

namespace BloodLoop.Application.Donations.Queries.GetDonationsSummary
{
    public class GetTotalWholeBloodSummaryQuery : IRequest<Either<Error, DonationSummaryDto>>
    {
        public AccountId AccountId { get; set; }

        public GetTotalWholeBloodSummaryQuery(AccountId accountId)
        {
            AccountId = accountId;
        }
    }
}