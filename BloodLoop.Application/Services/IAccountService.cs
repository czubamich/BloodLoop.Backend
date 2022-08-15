using System.Threading.Tasks;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donors;
using Microsoft.AspNetCore.Identity;

namespace BloodLoop.Application.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterDonorAsync(Donor donor, string password);
        Task<IdentityResult> RegisterStaffAsync(Domain.Staff.Staff staff, string password);

        Task<string> GetEmailVerificationToken(Account account);
        Task<IdentityResult> ConfirmEmailAsync(Account account, string token);
        Task<string> GetForgotPasswordToken(Account account);
        Task<IdentityResult> ResetPassword(Account account, string token, string password);
        Task<Account> GetAccountInfo(AccountId accountId);
        Task<Account> GetAccountInfoByEmail(string email);
    }
}