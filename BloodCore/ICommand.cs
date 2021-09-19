using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Cqrs
{
    public interface ICommand : IRequest
    {

    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {

    }
}
