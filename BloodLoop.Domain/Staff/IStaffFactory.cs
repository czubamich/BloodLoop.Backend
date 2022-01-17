using BloodLoop.Domain.BloodBanks;

namespace BloodLoop.Domain.Staff
{
    public interface IStaffFactory
    {
        Staff Create(string userName, string email, BloodBank bloodBank);
    }
}