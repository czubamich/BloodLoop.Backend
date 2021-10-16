using System;
using BloodCore.Results;
using LanguageExt;
using MediatR;

namespace BloodLoop.Application.Accounts.Queries
{
    public class GetDonorInfoQuery : IRequest<Either<Error, DonorDto>>
    {
        public Guid DonorId { get; set; }

        public GetDonorInfoQuery(Guid donorId)
        {
            DonorId = donorId;
        }
    }
}
