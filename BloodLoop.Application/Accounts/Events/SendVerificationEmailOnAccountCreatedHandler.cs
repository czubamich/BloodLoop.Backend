using BloodCore.Domain;
using BloodCore.Emails;
using BloodCore.Workers;
using BloodLoop.Application.Emails;
using BloodLoop.Application.Services;
using BloodLoop.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloodLoop.Application.Accounts.Events
{
    public class SendVerificationEmailOnAccountCreatedHandler : IDomainEventHandler<AccountCreatedEvent>
    {
        private readonly IAccountService _accountService;
        private readonly IJobScheduler _jobScheduler;
        private readonly IUrlService _urlService;

        public SendVerificationEmailOnAccountCreatedHandler(IAccountService accountService, IJobScheduler jobScheduler, IUrlService urlService)
        {
            _accountService = accountService;
            _jobScheduler = jobScheduler;
            _urlService = urlService;
        }

        public async Task Handle(AccountCreatedEvent notification, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountInfo(notification.AccountId);
            var confirmationToken = await _accountService.GetEmailVerificationToken(account);
            var confirmationUrl = _urlService.CreateWebUrl($"account/confirm/?accountId={account.Id}&confirmToken={confirmationToken}");

            var emailTemplate = new ConfirmEmailTemplate(account.FirstName, confirmationToken);

            _jobScheduler.Enqueue<IEmailService>(x => x.SendEmail(emailTemplate, account.Email), out _);
        }
    }
}
