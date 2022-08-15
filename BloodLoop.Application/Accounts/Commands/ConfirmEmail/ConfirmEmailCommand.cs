using System;
using BloodCore.Cqrs;
using BloodCore.Results;
using BloodLoop.Application.Accounts;
using BloodLoop.Domain.Donors;
using BloodLoop.Domain.Donations;
using LanguageExt;
using BloodLoop.Domain.Accounts;

namespace BloodLoop.Application.Donations.Commands
{
    public class ConfirmEmailCommand : ICommand<Either<Error, MediatR.Unit>>
    {
        public AccountId AccountId { get; set; }
        public string ConfirmationToken { get; set; }
    }
}