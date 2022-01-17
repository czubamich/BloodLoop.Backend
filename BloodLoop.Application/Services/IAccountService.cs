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

        Task<string> SendVerificationEmail(Account account);
        Task<IdentityResult> ConfirmEmailAsync(Account account, string code);
        Task<string> ForgotPassword(string email);
        Task<IdentityResult> ResetPassword(Account account, string token, string password);
    }
}