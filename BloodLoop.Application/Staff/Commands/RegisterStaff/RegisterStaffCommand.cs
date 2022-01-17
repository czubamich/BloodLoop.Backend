using BloodCore.Cqrs;
using BloodCore.Results;
using LanguageExt;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Application.Staff.Shared;

namespace BloodLoop.Application.Staff.Commands
{
    public class RegisterStaffCommand : ICommand<Either<Error, MediatR.Unit>>
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public BloodBankId bloodBankId { get; set; }

    }
}