using System;
using System.Threading.Tasks;
using BloodLoop.Domain.Accounts;
using Xunit;

namespace BloodLoop.Domain.Tests
{
    public class IdentityTests
    {
        [Fact]
        public void should_create_new_accountId()
        {
            //Arrange
            AccountId accountId = AccountId.New;

            //Assert
            Assert.NotNull(accountId);
            Assert.True(accountId.Id != Guid.Empty);
        }

        [Fact]
        public void should_create_accountId_from_guid()
        {
            //Arrange
            Guid guid = new Guid("a64b569e-4342-4d77-b3b0-7da5bf842eca");
            AccountId accountId = AccountId.Of(guid);

            //Assert
            Assert.NotNull(accountId);
            Assert.True(accountId.Id != Guid.Empty);
            Assert.True(accountId.Id == guid);
        }

        [Fact]
        public void should_create_accountId_from_string()
        {
            //Arrange
            string guid = "a64b569e-4342-4d77-b3b0-7da5bf842eca";
            AccountId accountId = AccountId.Of(guid);

            //Assert
            Assert.NotNull(accountId);
            Assert.True(accountId.Id != Guid.Empty);
            Assert.True(accountId.Id == new Guid(guid));
        }
    }
}
