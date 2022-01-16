using BloodCore.Results;
using LanguageExt;
using MediatR;
using System;
using Unit = MediatR.Unit;

namespace BloodLoop.Application.Donations.Queries.GetDonationConversion
{
    public sealed class GetDonationConversionQuery : IRequest<Either<Error, double>>
    {
        public string FromType { get; set; }
        public string ToType { get; set; }
    }
}