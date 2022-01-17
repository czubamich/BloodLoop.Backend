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

namespace BloodLoop.Application.BloodBanks.Commands
{
    public class RegisterStaffCommandValidator : AbstractValidator<RegisterBloodBankCommand>
    {
        public RegisterStaffCommandValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}

#pragma warning restore CS0618 // Type or member is obsolete
