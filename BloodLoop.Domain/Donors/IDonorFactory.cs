using System;
using BloodLoop.Domain.Accounts;

namespace BloodLoop.Domain.Donors
{
    public interface IDonorFactory
    {
        Donor Create(string userName, string email, string firstName, string lastName, GenderType gender, DateTime birthDay);
        Donor Create(string userName, string email, string firstName, string lastName, GenderType gender, DateTime birthDay, Pesel pesel);
    }
}