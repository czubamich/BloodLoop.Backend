using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Donations.Shared;
using BloodLoop.Application.Specifications.Accounts;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using FluentValidation;
using FluentValidation.AspNetCore;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Unit = LanguageExt.Unit;

namespace BloodLoop.Application.Donations.Queries.GetDonationsSummary
{
    public class GetDonationsSummaryQueryValidator : AbstractValidator<GetDonationsSummaryQuery>
    {
        public GetDonationsSummaryQueryValidator()
        {
            RuleFor(x => x.DonationTypeLabel).NotEmpty().Must(x => DonationType.GetDonationTypes().Any(dt => x.Equals(dt.Label,StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}