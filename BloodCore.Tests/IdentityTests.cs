using System;
using System.Threading.Tasks;
using BloodCore;
using BloodCore.Domain;
using Xunit;

namespace BloodCore.Tests
{
    public class IdentityTests
    {
        public class TestId : Identity<TestId>
        {
            private TestId(Guid id) : base(id) {}
        }

        [Fact]
        public void should_create_new_testId()
        {
            //Arrange
            TestId testId = TestId.New;

            //Assert
            Assert.NotNull(testId);
            Assert.True(testId.Id != Guid.Empty);
        }

        [Fact]
        public void should_create_testId_from_guid()
        {
            //Arrange
            Guid guid = new Guid("a64b569e-4342-4d77-b3b0-7da5bf842eca");
            TestId testId = TestId.Of(guid);

            //Assert
            Assert.NotNull(testId);
            Assert.True(testId.Id != Guid.Empty);
            Assert.True(testId.Id == guid);
        }

        [Fact]
        public void should_create_testId_from_string()
        {
            //Arrange
            string guid = "a64b569e-4342-4d77-b3b0-7da5bf842eca";
            TestId testId = TestId.Of(guid);

            //Assert
            Assert.NotNull(testId);
            Assert.True(testId.Id != Guid.Empty);
            Assert.True(testId.Id == new Guid(guid));
        }
    }
}
