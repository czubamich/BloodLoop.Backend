using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BloodCore.Results;
using BloodLoop.Application.Accounts;
using BloodLoop.Application.BloodBanks.Shared;
using BloodLoop.Application.Services;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donations;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BloodLoop.Application.BloodBanks.Commands
{
    public class RegisterBloodBankCommandHandler : IRequestHandler<RegisterBloodBankCommand, Either<Error, BloodBankDto>>
    {
        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IMapper _mapper;

        public RegisterBloodBankCommandHandler(IMapper mapper, IBloodBankRepository bloodBankRepository)
        {
            _mapper = mapper;
            _bloodBankRepository = bloodBankRepository;
        }

        public async Task<Either<Error, BloodBankDto>> Handle(RegisterBloodBankCommand request, CancellationToken cancellationToken)
        {
            var bloodBank = BloodBank.Create(request.Name, request.Address);

            await _bloodBankRepository.AddAsync(bloodBank, cancellationToken);

            return _mapper.Map<BloodBankDto>(bloodBank);
        }
    }
}