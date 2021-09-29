using System;
using BloodLoop.Domain.Accounts;

namespace BloodLoop.Domain.Donors
{
    public interface IDonorFactory
    {
        Donor Create(string userName, string email, GenderType gender, string firstName, string lastName,
            DateTime birthDay);
        Donor Create(string userName, string email, GenderType gender, string firstName, string lastName,
            DateTime birthDay, Pesel pesel);
    }
}