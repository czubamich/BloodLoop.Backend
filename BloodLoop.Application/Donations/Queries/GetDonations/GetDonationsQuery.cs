using System.Collections.Generic;
using BloodCore.Results;
using BloodLoop.Domain.Accounts;
using LanguageExt;
using MediatR;

namespace BloodLoop.Application.Donations.Queries.GetDonations
{
    public class GetDonationsQuery : IRequest<Either<Error, IEnumerable<DonationDto>>>
    {
        public AccountId AccountId { get; set; }

        public GetDonationsQuery(AccountId accountId)
        {
            AccountId = accountId;
        }
    }
}