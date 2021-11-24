using BloodCore.Results;
using LanguageExt;
using MediatR;
using System;

namespace BloodLoop.Application.Donations.Queries.GetDonationInterval
{
    public sealed class GetDonationIntervalQuery : IRequest<Either<Error, TimeSpan>>
    {
        public string FromType { get; set; }
        public string ToType { get; set; }
    }
}