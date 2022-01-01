using BloodCore.Persistance;
using BloodLoop.Application.Donations.Queries.GetDonationInterval;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Conversions;
using BloodLoop.Domain.Donations;
using BloodLoop.Domain.Donors;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace BloodLoop.Application.Tests
{
    public class GetDonationIntervalQueryHandlerTests
    {
        [Fact]
        public async Task ShouldGetIntervalWhenTimeLeft()
        {
            //Arrange
            var donationIntervalServiceMock = new Mock<IDonationIntervalService>();
            var readOnlyContextMock = new Mock<IReadOnlyContext>();
            var accountId = AccountId.New;
            var latestDonation = Donation.Create(DonorId.Of(accountId.Id), DonationType.Platelets.Label, DateTime.Now - TimeSpan.FromDays(7));

            donationIntervalServiceMock.Setup(x => 
                x.Convert(
                    It.Is<DonationType>(x => x.Label == DonationType.Platelets.Label),
                    It.Is<DonationType>(x => x.Label == DonationType.Whole.Label))
                )
                .ReturnsAsync(TimeSpan.FromDays(14))
                .Verifiable();

            var userDonations = new List<Donation>() { latestDonation };
            readOnlyContextMock.Setup(x => x.GetQueryable(It.IsAny<Expression<Func<Donation, string>>[]>()))
                .Returns(userDonations.AsQueryable().BuildMock().Object)
                .Verifiable();

            var donationTypes = new List<DonationType>() { DonationType.Platelets, DonationType.Whole };
            readOnlyContextMock.Setup(x => x.GetQueryable(It.IsAny<Expression<Func<DonationType, string>>[]>()))
                .Returns(donationTypes.AsQueryable().BuildMock().Object)
                .Verifiable();

            //Act
            var request = new GetDonationIntervalForUserQuery(accountId, DonationType.Whole.Label);
            var handler = new GetDonationIntervalForUserQueryHandler(donationIntervalServiceMock.Object, readOnlyContextMock.Object);

            var response = await handler.Handle(request, default);

            readOnlyContextMock.Verify();
            donationIntervalServiceMock.Verify();

            //Assert
            response.IsRight.Should().BeTrue();
            response.Right(r => r.Should().Be(TimeSpan.FromDays(7)));
        }
    }
}
