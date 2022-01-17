using BloodLoop.Domain.Donors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Staff.Commands.VerifyDonor
{
    public class VerifyDonorCommandValidator : AbstractValidator<VerifyDonorCommand>
    {
        public VerifyDonorCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex);
            
            RuleFor(x => x.Pesel)
                .NotEmpty()
                .Must(x => Pesel.TryParse(x, out _));
        }
    }
}
