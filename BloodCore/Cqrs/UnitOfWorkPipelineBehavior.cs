using BloodCore.Cqrs;
using BloodCore.Persistance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloodCore.Api
{
    class UnitOfWorkPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkPipelineBehavior(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is ICommand<TResponse> || request is ICommand)
            {
                await _unitOfWork.BeginTransactionAsync();

                try
                {
                    var result = await next();

                    foreach(var domainEvent in _unitOfWork.Aggregates.SelectMany(x => x.DomainEvents).ToList())
                    {
                        await _mediator.Publish(domainEvent, cancellationToken);
                    }

                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();

                    return result;
                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    throw;
                }
            }

            return await next();
        }
    }
}
