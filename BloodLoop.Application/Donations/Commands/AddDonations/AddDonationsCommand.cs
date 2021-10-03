using BloodCore.Cqrs;
using BloodCore.Results;
using BloodLoop.Domain.Accounts;
using LanguageExt;

namespace BloodLoop.Application.Donations.Commands.AddDonations
{
    public class AddDonationsCommand : ICommand<Either<Error, DonationDto[]>>
    {
        public AccountId AccountId { get; set; }
        public DonationDto[] NewDonations { get; set; }

        public AddDonationsCommand(AccountId accountId, DonationDto[] newDonations)
        {
            AccountId = accountId;
            NewDonations = newDonations;
        }
    }
}