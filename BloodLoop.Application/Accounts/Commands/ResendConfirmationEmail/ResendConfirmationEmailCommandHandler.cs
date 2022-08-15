using BloodCore.Api;
using BloodCore.Emails;
using BloodCore.Results;
using BloodCore.Workers;
using BloodLoop.Application.Donations.Commands;
using BloodLoop.Application.Emails;
using BloodLoop.Application.Services;
using LanguageExt;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace BloodLoop.Application.Accounts.Commands
{
    public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand, Either<Error, Unit>>
    {
        private readonly IAccountService _accountService;
        private readonly IJobScheduler _jobScheduler;
        private readonly IUrlService _urlService;

        public ResendConfirmationEmailCommandHandler(IAccountService accountService, IJobScheduler jobScheduler, IUrlService urlService)
        {
            _accountService = accountService;
            _jobScheduler = jobScheduler;
            _urlService = urlService;
        }

        public async Task<Either<Error, Unit>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountInfoByEmail(request.Email);
            
            if(account is null)
                return Unit.Value;

            if(account.EmailConfirmed)
                return Result.Invalid<Unit>("Email already confirmed!");

            var confirmationToken = await _accountService.GetEmailVerificationToken(account);
            var confirmationUrl = _urlService.CreateWebUrl($"account/confirm/?accountId={account.Id}&confirmToken={confirmationToken}");

            var emailTemplate = new ConfirmEmailTemplate(account.FirstName, confirmationUrl);

            _jobScheduler.Enqueue<IEmailService>(x => x.SendEmail(emailTemplate, account.Email), out _);

            return Unit.Value;
        }
    }
}
