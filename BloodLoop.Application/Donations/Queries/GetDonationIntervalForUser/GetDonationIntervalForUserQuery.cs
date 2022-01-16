using BloodCore.Results;
using BloodLoop.Domain.Accounts;
using LanguageExt;
using MediatR;
using System;

namespace BloodLoop.Application.Donations.Queries.GetDonationInterval
{
    public sealed class GetDonationIntervalForUserQuery : IRequest<Either<Error, TimeSpan>>
    {
        public AccountId AccountId { get; set; }
        public string ToType { get; set; }

        public GetDonationIntervalForUserQuery(AccountId accountId, string toType)
        {
            AccountId = accountId;
            ToType = toType;
        }
    }
}