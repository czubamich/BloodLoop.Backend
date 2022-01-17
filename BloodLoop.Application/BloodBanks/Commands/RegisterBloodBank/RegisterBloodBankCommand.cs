using System;
using BloodCore.Cqrs;
using BloodCore.Results;
using BloodLoop.Application.Accounts;
using BloodLoop.Domain.Donations;
using LanguageExt;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Application.BloodBanks.Shared;

namespace BloodLoop.Application.BloodBanks.Commands
{
    public class RegisterBloodBankCommand : ICommand<Either<Error, BloodBankDto>>
    {
        public string Name { get; set; }
        public string Address { get; set; }

    }
}