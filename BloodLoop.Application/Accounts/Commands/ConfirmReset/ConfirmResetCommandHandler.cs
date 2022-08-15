using BloodCore.Api;
using BloodCore.Results;
using BloodLoop.Application.Donations.Commands;
using BloodLoop.Application.Services;
using System.Linq;
using LanguageExt;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Unit = MediatR.Unit;
using BloodCore.Extensions;
using BloodLoop.Domain.Accounts;

namespace BloodLoop.Application.Accounts.Commands
{
    public class ConfirmResetCommandHandler : IRequestHandler<ConfirmResetCommand, Either<Error, Unit>>
    {
        private readonly IAccountService _accountService;
        private readonly IUrlService _urlService;

        public ConfirmResetCommandHandler(IAccountService accountService, IUrlService urlService)
        {
            _accountService = accountService;
            _urlService = urlService;
        }

        public async Task<Either<Error, Unit>> Handle(ConfirmResetCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountInfo(AccountId.Of(request.AccountId));

            if (account is null)
                return Result.Invalid<Unit>("Invalid reset token!");

            var result = await _accountService.ResetPassword(account, request.ResetToken, request.Password);

            if(!result.Succeeded)
            {
                var errorResult = new Error.Validation();
                result.Errors.ForEach(e => errorResult.AddError(nameof(request.ResetToken), e.Description));

                return errorResult;
            }

            return Unit.Value;
        }
    }
}
