using System;
using BloodCore.Cqrs;
using BloodCore.Results;
using BloodLoop.Application.Accounts;
using BloodLoop.Domain.Donors;
using BloodLoop.Domain.Donations;
using LanguageExt;

namespace BloodLoop.Application.Donations.Commands
{
    public class ResendConfirmationEmailCommand : ICommand<Either<Error, MediatR.Unit>>
    {
        public string Email { get; set; }
    }
}