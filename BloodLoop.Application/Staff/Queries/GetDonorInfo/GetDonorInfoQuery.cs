using BloodCore.Results;
using BloodLoop.Application.Staff.Shared;
using LanguageExt;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Application.Staff.Queries.GetDonorInfo
{
    public class GetDonorInfoQuery : IRequest<Either<Error, DonorExtDto>>
    {
        public string EmailOrPesel { get; set; }
    }
}
