using System;
using BloodCore;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Infrastructure.Factories
{
    [Injectable(IsScoped = true)]
    class DonorFactory : IDonorFactory
    {
        public Donor Create(string email, string firstName, string lastName,
            DateTime birthDay, GenderType gender, BloodType bloodType)
        {
            Account account = Account.Create(email, email)
                .SetFirstName(firstName)
                .SetLastName(lastName);

            return Donor.Create(DonorId.Of(account.Id), account, gender, birthDay)
                .SetBloodType(bloodType);
        }
    }
}
