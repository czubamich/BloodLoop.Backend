using BloodLoop.Domain.Donors;
using System;
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
        [InlineData("16062338447")]
        [InlineData("03021112233")]
        [InlineData("14051486672")]
        [InlineData("10030532978")]
        [InlineData("01110686393")]
        public void pesel_validation_succeed_when_is_valid(string peselString)
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
        [InlineData("16062338446")]
        [InlineData("03021112237")]
        [InlineData("14051486678")]
        [InlineData("10030532979")]
        [InlineData("01110686390")]
        public void pesel_validation_fails_when_is_invalid(string peselString)
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
