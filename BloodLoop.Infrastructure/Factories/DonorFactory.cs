using System;
using BloodCore;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donors;

namespace BloodLoop.Infrastructure.Factories
{
    [Injectable]
    class DonorFactory : IDonorFactory
    {
        public Donor Create(string userName, string email, string firstName, string lastName, 
            GenderType gender, DateTime birthDay)
        {
            Account account = Account.Create(userName, email)
                .SetFirstName(firstName)
                .SetLastName(lastName);

            return Donor.Create(account, gender, birthDay);
        }

        public Donor Create(string userName, string email, string firstName, string lastName, 
            GenderType gender, DateTime birthDay, Pesel pesel)
        {
            Donor donor = Create(userName, email, firstName, lastName, gender, birthDay);

            donor.SetPesel(pesel);

            return donor;
        }
    }
}
