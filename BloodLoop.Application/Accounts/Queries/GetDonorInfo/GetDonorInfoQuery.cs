using System;
using BloodCore.Results;
using BloodLoop.Domain.Donors;
using LanguageExt;
using MediatR;

namespace BloodLoop.Application.Accounts.Queries
{
    public class GetDonorInfoQuery : IRequest<Either<Error, DonorDto>>
    {
        public DonorId DonorId { get; set; }

        public GetDonorInfoQuery(DonorId donorId)
        {
            DonorId = donorId;
        }
    }
}
