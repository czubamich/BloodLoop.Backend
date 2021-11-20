using System;
using BloodCore.Cqrs;
using BloodCore.Results;
using BloodLoop.Application.Accounts;
using BloodLoop.Domain.Donors;
using BloodLoop.Domain.Donations;
using LanguageExt;

namespace BloodLoop.Application.Donations.Commands
{
    public class RegisterDonorCommand : ICommand<Either<Error, DonorDto>>
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public GenderType Gender { get; set; }
        public string BloodTypeLabel { get; set; }

        public DateTime BirthDay { get; set; }

    }
}