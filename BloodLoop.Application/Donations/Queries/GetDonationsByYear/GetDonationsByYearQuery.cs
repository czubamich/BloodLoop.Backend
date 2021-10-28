using System.Collections.Generic;
using BloodCore.Results;
using BloodLoop.Domain.Accounts;
using LanguageExt;
using MediatR;

namespace BloodLoop.Application.Donations.Queries.GetDonations
{
    public class GetDonationsByYearQuery : IRequest<Either<Error, IEnumerable<DonationGroupDto>>>
    {
        public AccountId AccountId { get; set; }

        public GetDonationsByYearQuery(AccountId accountId)
        {
            AccountId = accountId;
        }
    }
}