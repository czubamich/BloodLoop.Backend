using BloodCore.Cqrs;
using BloodCore.Results;
using LanguageExt;

namespace BloodLoop.Application.Staff.Commands.VerifyDonor
{
    public class VerifyDonorCommand : ICommand<Either<Error, MediatR.Unit>>
    {
        public string Email { get; set; }
        public string Pesel { get; set; }
    }
}
