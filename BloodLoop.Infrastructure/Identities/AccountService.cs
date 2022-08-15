using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using BloodCore;
using BloodLoop.Application.Services;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donors;
using BloodLoop.Domain.Staff;
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
        private readonly IStaffRepository _staffRepository;
        private readonly IMediator _mediator;

        public AccountService(UserManager<Account> userManager,
            RoleManager<Role> roleManager,
            IDonorRepository donorRepository,
            IStaffRepository staffRepository,
            IMediator mediator)
        {
            _userManager = userManager;
            _donorRepository = donorRepository;
            _staffRepository = staffRepository;
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

        public async Task<IdentityResult> RegisterStaffAsync(Staff staff, string password)
        {
            var result = await _userManager.CreateAsync(staff.Account, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(staff.Account, Role.Staff.ToString());
                await _userManager.AddClaimAsync(staff.Account, new Claim(nameof(BloodBankId), staff.BloodBankId.Id.ToString()));

                await _staffRepository.AddAsync(staff);
            }

            //TODO: Publish domain event
            return result;
        }

        public async Task<string> GetEmailVerificationToken(Account account)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(account);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(Account account, string token)
        {
            var user = await _userManager.FindByIdAsync(account.Id.ToString());
            
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GetForgotPasswordToken(Account account)
        {
            if (account == null) return null;

            return await _userManager.GeneratePasswordResetTokenAsync(account);
        }

        public async Task<IdentityResult> ResetPassword(Account account, string token, string password)
        {
            if (await _userManager.FindByEmailAsync(account.Email) != account)
                throw new ValidationException($"No Accounts Registered with {account.Email}.");
            
            return await _userManager.ResetPasswordAsync(account, token, password);
        }

        public Task<Account> GetAccountInfo(AccountId accountId)
        {
            return _userManager.FindByIdAsync(accountId.Id.ToString());
        }

        public Task<Account> GetAccountInfoByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }
    }
}