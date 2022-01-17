using BloodCore;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donors;
using BloodLoop.Domain.Staff;

namespace BloodLoop.Infrastructure.Factories
{
    [Injectable(IsScoped = true)]
    class StaffFactory : IStaffFactory
    {
        public Staff Create(string userName, string email, BloodBank bloodBank)
        {
            Account account = Account.Create(userName, email);

            return Staff.Create(StaffId.Of(account.Id), account, bloodBank);
        }
    }
}
