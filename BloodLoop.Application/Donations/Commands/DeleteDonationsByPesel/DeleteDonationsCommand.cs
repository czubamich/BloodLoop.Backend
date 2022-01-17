using BloodCore.Cqrs;
using BloodCore.Results;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Staff.Commands.DeleteDonations
{
    public class DeleteDonationsCommand : ICommand<Either<Error, MediatR.Unit>>
    {
        public IEnumerable<DeleteDonationRequest> DonationsToDelete { get; set; }

        public DeleteDonationsCommand(IEnumerable<DeleteDonationRequest> donationsToDelete)
        {
            DonationsToDelete = donationsToDelete;
        }
    }
}
