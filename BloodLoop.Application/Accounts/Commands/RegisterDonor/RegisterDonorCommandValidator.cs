using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using FluentValidation;
using FluentValidation.Validators;

#pragma warning disable CS0618 // Type or member is obsolete

namespace BloodLoop.Application.Donations.Commands
{
    public class RegisterDonorCommandValidator : AbstractValidator<RegisterDonorCommand>
    {
        public RegisterDonorCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress(EmailValidationMode.Net4xRegex);

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .Equal(x => x.ConfirmPassword);

            RuleFor(x => x.BirthDay)
                .LessThanOrEqualTo(DateTime.UtcNow.AddYears(-17))
                .GreaterThan(DateTime.UtcNow.AddYears(-100));

            RuleFor(x => x.BloodTypeLabel)
                .Must(btl => BloodType.GetBloodTypes().Any(bt => bt.Label == btl));

            RuleFor(x => x.Gender)
                .IsInEnum();
        }
    }
}

#pragma warning restore CS0618 // Type or member is obsolete
