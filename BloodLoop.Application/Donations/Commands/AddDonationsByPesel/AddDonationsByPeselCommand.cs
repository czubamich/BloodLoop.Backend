using BloodCore.Cqrs;
using BloodCore.Results;
using LanguageExt;

namespace BloodLoop.Application.Donations.Commands.AddDonations
{
    public class AddDonationsByPeselCommand : ICommand<Either<Error, MediatR.Unit>>
    {
        public DonationWithPeselDto[] NewDonations { get; set; }

        public AddDonationsByPeselCommand(DonationWithPeselDto[] newDonations)
        {
            NewDonations = newDonations;
        }
    }
}