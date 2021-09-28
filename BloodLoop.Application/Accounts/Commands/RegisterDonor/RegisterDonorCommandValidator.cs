using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodLoop.Domain.Donors;
using FluentValidation;
using FluentValidation.Validators;

namespace BloodLoop.Application.Donations.Commands.RegisterDonor
{
    public class RegisterDonorCommandValidator : AbstractValidator<RegisterDonorCommand>
    {
        public RegisterDonorCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
#pragma warning disable CS0618 // Type or member is obsolete
                .EmailAddress(EmailValidationMode.Net4xRegex);
#pragma warning restore CS0618 // Type or member is obsolete

            RuleFor(x => x.UserName)
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.BirthDay)
                .GreaterThan(DateTime.UtcNow.AddYears(-100));

            RuleFor(x => x.Gender)
                .IsInEnum();
        }
    }
}
