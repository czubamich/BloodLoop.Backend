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
    public class ConfirmResetCommand : ICommand<Either<Error, MediatR.Unit>>
    {
        public Guid AccountId { get; set; }
        public string ResetToken { get; set; }
        public string Password { get; set; }
    }
}