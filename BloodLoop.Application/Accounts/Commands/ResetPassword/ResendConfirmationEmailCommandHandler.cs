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
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Either<Error, Unit>>
    {
        private readonly IAccountService _accountService;
        private readonly IJobScheduler _jobScheduler;
        private readonly IUrlService _urlService;

        public ResetPasswordCommandHandler(IAccountService accountService, IJobScheduler jobScheduler, IUrlService urlService)
        {
            _accountService = accountService;
            _jobScheduler = jobScheduler;
            _urlService = urlService;
        }

        public async Task<Either<Error, Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountInfoByEmail(request.Email);
            
            if(account is null)
                return Unit.Value;

            var resetToken = await _accountService.GetForgotPasswordToken(account);
            var resetUrl = _urlService.CreateWebUrl($"account/reset/?accountId={account.Id}&resetToken={resetToken}");

            var emailTemplate = new ForgotPasswordEmailTemplate(account.FirstName, resetUrl);

            _jobScheduler.Enqueue<IEmailService>(x => x.SendEmail(emailTemplate, account.Email), out _);

            return Unit.Value;
        }
    }
}
