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

namespace BloodLoop.Application.Accounts.Commands
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Either<Error, Unit>>
    {
        private readonly IAccountService _accountService;
        private readonly IUrlService _urlService;

        public ConfirmEmailCommandHandler(IAccountService accountService, IUrlService urlService)
        {
            _accountService = accountService;
            _urlService = urlService;
        }

        public async Task<Either<Error, Unit>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountInfo(request.AccountId);

            if (account is null)
                return Result.NotFound<Unit>("Invalid confirmation link!");

            if (account.EmailConfirmed)
                return Result.Invalid<Unit>("Email already confirmed!");

            var result = await _accountService.ConfirmEmailAsync(account, request.ConfirmationToken);

            if(!result.Succeeded)
            {
                var errorResult = new Error.Validation();
                result.Errors.ForEach(e => errorResult.AddError(nameof(request.ConfirmationToken), e.Description));

                return errorResult;
            }

            return Unit.Value;
        }
    }
}
