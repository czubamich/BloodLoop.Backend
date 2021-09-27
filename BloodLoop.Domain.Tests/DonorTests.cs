using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodCore.Domain;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donors;
using FluentAssertions;
using Xunit;

namespace BloodLoop.Domain.Tests
{
    public class DonorTests
    {
        [Fact]
        public void should_create_donor()
        {
            //Arrange
            var accountId = AccountId.New;
            var gender = GenderType.Male;
            var birthDay = DateTime.Parse("1990-01-01T00:00:00.0000000-00:00");
            var pesel = new Pesel("04032514889");

            //Act
            var donor = Donor.Create(accountId, gender, birthDay)
                .SetPesel(pesel);

            //Assert
            donor.Id.Should().NotBeNull();
            donor.AccountId.Should().Be(accountId);
            donor.Gender.Should().Be(gender);
            donor.BirthDay.Should().Be(birthDay);
            donor.Pesel.Should().Be(pesel);
        }

        public void should_not_overwrite_pesel()
        {
            //Arrange
            var pesel = new Pesel("04032514889");
            var donor = Donor.Create(AccountId.New, GenderType.Male, DateTime.Parse("1990-01-01T00:00:00.0000000-00:00"))
                .SetPesel(pesel);

            //Act

            //Assert
            donor.Should().Invoking(_ => donor.SetPesel(pesel)).Should().Throw<DomainException>();
        }
    }
}
