using System.Threading.Tasks;
using BloodCore;
using BloodLoop.Application.Services;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donors;
using BloodLoop.Infrastructure.Persistance;
using BloodLoop.Infrastructure.Settings;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BloodLoop.Infrastructure.Identities
{
    [Injectable]
    public class AccountService : IAccountService
    {
        private readonly UserManager<Account> _userManager;
        private readonly IDonorRepository _donorRepository;
        private readonly IMediator _mediator;

        public AccountService(UserManager<Account> userManager,
            RoleManager<Role> roleManager,
            IDonorRepository donorRepository,
            IMediator mediator)
        {
            _userManager = userManager;
            _donorRepository = donorRepository;
            _mediator = mediator;
        }

        public async Task<IdentityResult> RegisterDonorAsync(Donor donor, string password)
        {
            var result = await _userManager.CreateAsync(donor.Account, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(donor.Account, Role.Donor.ToString());

                await _donorRepository.AddAsync(donor);
            }

            //TODO: Publish domain event
            return result;
        }

        public async Task<string> SendVerificationEmail(Account account)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(account);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(Account account, string code)
        {
            var user = await _userManager.FindByIdAsync(account.Id.ToString());
            
            return await _userManager.ConfirmEmailAsync(user, code);
        }

        public async Task<string> ForgotPassword(string email)
        {
            var account = await _userManager.FindByEmailAsync(email);

            if (account == null) return null;

            //TODO: Publish domain event
            return await _userManager.GeneratePasswordResetTokenAsync(account);
        }

        public async Task<IdentityResult> ResetPassword(Account account, string token, string password)
        {
            if (await _userManager.FindByEmailAsync(account.Email) != account)
                throw new ValidationException($"No Accounts Registered with {account.Email}.");
            
            return await _userManager.ResetPasswordAsync(account, token, password);
        }
    }
}