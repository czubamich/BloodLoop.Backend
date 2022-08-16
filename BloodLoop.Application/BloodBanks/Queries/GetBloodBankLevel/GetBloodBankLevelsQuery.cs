using System;
using BloodCore.Cqrs;
using BloodCore.Results;
using BloodLoop.Application.Accounts;
using BloodLoop.Domain.Donations;
using LanguageExt;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Application.BloodBanks.Shared;

namespace BloodLoop.Application.BloodBanks.Queries
{
    public class GetBloodBankLevelsQuery : ICommand<Either<Error, BloodLevelDto[]>>
    {
        public string BloodLevel { get; set; }

        public GetBloodBankLevelsQuery(string bloodLevel)
        {
            BloodLevel = bloodLevel;
        }
    }
}