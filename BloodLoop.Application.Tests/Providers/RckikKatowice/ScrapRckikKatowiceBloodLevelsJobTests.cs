using Ardalis.Specification;
using BloodCore.Persistance;
using BloodCore.Services;
using BloodLoop.Application.Jobs;
using BloodLoop.Application.Providers.RckikKatowice;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donations;
using FluentAssertions;
using Microsoft.AspNetCore.Components.RenderTree;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace BloodLoop.Application.Tests.Providers.RckikKatowice
{
    public class ScrapRckikKatowiceBloodLevelsJobTests
    {
        public ScrapRckikKatowiceBloodLevelsJob job;
        public IDateTimeService dateTimeService;
        public IRckikKatowiceClient client;
        public IUnitOfWork unitOfWork;
        public BloodBank bloodBank;

        public ScrapRckikKatowiceBloodLevelsJobTests()
        {
            var repository = Substitute.For<IBloodBankRepository>();
            dateTimeService = Substitute.For<IDateTimeService>();
            client = Substitute.For<IRckikKatowiceClient>();
            unitOfWork = Substitute.For<IUnitOfWork>();

            bloodBank = BloodBank.Create("rckik-katowice", "RCKiK Katowice", "Ul. Raciborska 15, 40-074 Katowice, Woj. Śląskie");

            repository.Get(Arg.Any<ISpecification<BloodBank>>())
                .ReturnsForAnyArgs(bloodBank);

            job = new ScrapRckikKatowiceBloodLevelsJob(unitOfWork, client, repository, dateTimeService);
        }

        [Fact]
        public async Task Job_AddsBloodLevels_WhenDataIsFresh()
        {
            //Arrange
            dateTimeService.Now().Returns(new DateTime(2022,6,1));

            client.GetBloodLevels()
                .ReturnsForAnyArgs(new List<RK_BloodRS>()
                {
                    new RK_BloodRS()
                    {
                        Slug = "a",
                        Date = new DateTime(2022,6,1),
                        Acf = new RK_AcfRS()
                        {
                            Level = "100"
                        }
                    }
                });

            //Act
            await job.ScrapBloodLevels();

            //Assert
            Received.InOrder(() =>
            {
                unitOfWork.BeginTransactionAsync();
                unitOfWork.SaveChangesAsync();
                unitOfWork.CommitTransactionAsync();
            });

            bloodBank.BloodLevels.Should().NotBeEmpty();
            
            var bloodLevel = bloodBank.BloodLevels.First();
            bloodLevel.BloodBankId.Should().Be(bloodBank.Id);
            bloodLevel.BloodType.Should().BeEquivalentTo(BloodType.A_Rh_Plus);
            bloodLevel.Measurement.Should().Be(100);
            bloodLevel.BloodBankId.Should().Be(bloodBank.Id);
        }
       
    }
}
