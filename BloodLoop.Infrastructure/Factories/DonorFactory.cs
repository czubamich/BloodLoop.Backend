using System;
using BloodCore;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Infrastructure.Factories
{
    [Injectable(IsScoped = true)]
    class DonorFactory : IDonorFactory
    {
        public Donor Create(string userName, string email, GenderType gender, 
            string firstName, string lastName, DateTime birthDay)
        {
            Account account = Account.Create(userName, email)
                .SetFirstName(firstName)
                .SetLastName(lastName);

            return Donor.Create(DonorId.Of(account.Id), account, gender, birthDay);
        }
    }
}
