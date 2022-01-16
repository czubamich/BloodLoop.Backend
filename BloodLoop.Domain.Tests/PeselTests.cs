using BloodLoop.Domain.Donors;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BloodLoop.Domain.Tests
{
    public class PeselTests
    {
        [Theory]
        [InlineData("04032514889")]
        [InlineData("04032394681")]
        [InlineData("17010714272")]
        [InlineData("19061479265")]
        [InlineData("07030248938")]
        public void Pesel_validation_succeed_when_is_valid(string peselString)
        {
            //Arrange
            Pesel pesel = new Pesel(peselString);

            //Act
            var isValidPesel = pesel.IsValid();

            //Assert
            Assert.True(isValidPesel);
        }

        [Theory]
        [InlineData("04032514881")]
        [InlineData("04032394682")]
        [InlineData("17010714273")]
        [InlineData("19061479264")]
        [InlineData("07030248935")]
        public void Pesel_validation_fails_when_is_invalid(string peselString)
        {
            //Arrange
            Pesel pesel = new Pesel(peselString);

            //Act
            var isValidPesel = pesel.IsValid();

            //Assert
            Assert.False(isValidPesel);
        }
    }
}
