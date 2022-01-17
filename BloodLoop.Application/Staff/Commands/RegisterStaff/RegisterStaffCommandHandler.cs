using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Persistance;
using BloodCore.Results;
using BloodLoop.Application.Accounts;
using BloodLoop.Application.Services;
using BloodLoop.Application.Staff.Shared;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Staff;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloodLoop.Application.Staff.Commands
{
    public class RegisterStaffCommandHandler : IRequestHandler<RegisterStaffCommand, Either<Error, MediatR.Unit>>
    {
        private readonly IAccountService _accountService;
        private readonly IReadOnlyContext _readOnlyContext;
        private readonly IStaffFactory _staffFactory;
        private readonly IMapper _mapper;

        public RegisterStaffCommandHandler(
            IAccountService accountService, 
            IStaffFactory staffFactory, 
            IMapper mapper, 
            IReadOnlyContext readOnlyContext)
        {
            _accountService = accountService;
            _staffFactory = staffFactory;
            _mapper = mapper;
            _readOnlyContext = readOnlyContext;
        }

        public async Task<Either<Error, MediatR.Unit>> Handle(RegisterStaffCommand request, CancellationToken cancellationToken)
        {
            var bloodBank = await _readOnlyContext.GetQueryable<BloodBank>().FirstOrDefaultAsync(x => x.Id == BloodBankId.Of(request.bloodBankId));

            if (bloodBank is null)
            {
                return new Error.Validation()
                    .AddError(nameof(request.bloodBankId), "Invalid Id");
            }

            var newStaff = _staffFactory.Create(
                    request.Username,
                    request.Email,
                    bloodBank);

            var identityResult = await _accountService.RegisterStaffAsync(newStaff, request.Password);

            if (!identityResult.Succeeded)
            {
                var validationError = new Error.Validation();
                AddErrorsFromIdentityResult(validationError, identityResult);

                return validationError;
            }

            return MediatR.Unit.Value;
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
                    return nameof(RegisterStaffCommand.Username);
                case nameof(IdentityErrorDescriber.InvalidEmail):
                case nameof(IdentityErrorDescriber.DuplicateEmail):
                    return nameof(RegisterStaffCommand.Email);
                case nameof(IdentityErrorDescriber.PasswordRequiresDigit):
                case nameof(IdentityErrorDescriber.PasswordRequiresLower):
                case nameof(IdentityErrorDescriber.PasswordRequiresNonAlphanumeric):
                case nameof(IdentityErrorDescriber.PasswordRequiresUpper):
                case nameof(IdentityErrorDescriber.PasswordTooShort):
                case nameof(IdentityErrorDescriber.PasswordRequiresUniqueChars):
                    return nameof(RegisterStaffCommand.Password);
                default:
                    return string.Empty;
            }
        }
    }
}