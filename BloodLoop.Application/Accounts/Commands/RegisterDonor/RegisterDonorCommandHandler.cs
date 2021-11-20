using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Accounts;
using BloodLoop.Application.Services;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BloodLoop.Application.Donations.Commands
{
    public class RegisterDonorCommandHandler : IRequestHandler<RegisterDonorCommand, Either<Error, DonorDto>>
    {
        private readonly IAccountService _accountService;
        private readonly IDonorFactory _donorFactory;
        private readonly IMapper _mapper;

        public RegisterDonorCommandHandler(IAccountService accountService, IDonorFactory donorFactory, IMapper mapper)
        {
            _accountService = accountService;
            _donorFactory = donorFactory;
            _mapper = mapper;
        }

        public async Task<Either<Error, DonorDto>> Handle(RegisterDonorCommand request, CancellationToken cancellationToken)
        {
            var newDonor = _donorFactory.Create(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.BirthDay,
                    request.Gender,
                    BloodType.GetBloodTypes().FirstOrDefault(x => x.Label == request.BloodTypeLabel));

            var identityResult = await _accountService.RegisterDonorAsync(newDonor, request.Password);

            if (!identityResult.Succeeded)
            {
                var validationError = new Error.Validation();
                AddErrorsFromIdentityResult(validationError, identityResult);

                return validationError;
            }

            return _mapper.Map<DonorDto>(newDonor);
        }

        private void AddErrorsFromIdentityResult(Error.Validation validationError, IdentityResult identityResult)
        {
            var errors =
                identityResult.Errors.Select(x => new
                {
                    PropertyName = MapIdentityErrorToPropertyName(x),
                    Error = x.Description
                });

            foreach (var error in errors)
            {
                validationError.AddError(error.PropertyName, error.Error);
            }
        }

        private string MapIdentityErrorToPropertyName(IdentityError identityError)
        {
            switch (identityError.Code)
            {
                case nameof(IdentityErrorDescriber.InvalidUserName):
                case nameof(IdentityErrorDescriber.DuplicateUserName):
                    return nameof(RegisterDonorCommand.UserName);
                case nameof(IdentityErrorDescriber.InvalidEmail):
                case nameof(IdentityErrorDescriber.DuplicateEmail):
                    return nameof(RegisterDonorCommand.Email);
                case nameof(IdentityErrorDescriber.PasswordRequiresDigit):
                case nameof(IdentityErrorDescriber.PasswordRequiresLower):
                case nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric):
                case nameof(IdentityErrorDescriber.PasswordRequiresUpper):
                case nameof(IdentityErrorDescriber.PasswordTooShort):
                case nameof(IdentityErrorDescriber.PasswordRequiresUniqueChars):
                    return nameof(RegisterDonorCommand.Password);
                default:
                    return string.Empty;
            }
        }
    }
}