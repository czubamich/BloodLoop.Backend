using System;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donations;

namespace BloodLoop.Domain.Donors
{
    public interface IDonorFactory
    {
        Donor Create(string email, string firstName, string lastName, DateTime birthDay, GenderType gender, BloodType bloodType);
    }
}