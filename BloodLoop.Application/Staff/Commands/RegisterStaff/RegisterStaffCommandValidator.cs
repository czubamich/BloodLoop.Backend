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

namespace BloodLoop.Application.Staff.Commands
{
    public class RegisterStaffCommandValidator : AbstractValidator<RegisterStaffCommand>
    {
        public RegisterStaffCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress(EmailValidationMode.Net4xRegex);

            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .Equal(x => x.ConfirmPassword);
        }
    }
}

#pragma warning restore CS0618 // Type or member is obsolete
