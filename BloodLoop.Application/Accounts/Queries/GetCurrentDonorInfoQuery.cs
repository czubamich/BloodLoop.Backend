using System;
using BloodCore.Results;
using LanguageExt;
using MediatR;

namespace BloodLoop.Application.Accounts.Queries
{
    public class GetCurrentDonorInfoQuery : IRequest<Either<Error, DonorDto>>
    {
        public Guid DonorId { get; set; }
    }
}
