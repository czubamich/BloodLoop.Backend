using BloodCore.Cqrs;
using BloodCore.Results;
using BloodLoop.Domain.Accounts;
using LanguageExt;

namespace BloodLoop.Application.Donations.Commands.AddDonations
{
    public class AddDonationsByPeselCommand : ICommand<Either<Error, AddDonationsByPeselResponse>>
    {
        public DonationWithPeselDto[] NewDonations { get; set; }

        public AddDonationsByPeselCommand(DonationWithPeselDto[] newDonations)
        {
            NewDonations = newDonations;
        }
    }
}