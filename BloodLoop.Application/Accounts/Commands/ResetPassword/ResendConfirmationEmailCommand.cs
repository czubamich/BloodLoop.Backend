using BloodCore.Cqrs;
using BloodCore.Results;
using LanguageExt;

namespace BloodLoop.Application.Donations.Commands
{
    public class ResetPasswordCommand : ICommand<Either<Error, MediatR.Unit>>
    {
        public string Email { get; set; }
    }
}