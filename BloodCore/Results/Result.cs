using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodCore.Common;
using BloodCore.Results;

namespace BloodCore.Api
{
    public static class Result
    {
        public static Either<Error, TValue> Success<TValue>(TValue value)
            => value;

        public static Either<Error, MediatR.Unit> Success()
            => MediatR.Unit.Value;

        public static Either<Error, TValue> Invalid<TValue>(string message)
            => new Error.Invalid(message);

        public static Either<Error, TValue> NotFound<TValue>(string message)
            => new Error.NotFound(message);

        public static Either<Error, TValue> Unauthorized<TValue>(string message)
            => new Error.Unauthorized(message);
    }
}
