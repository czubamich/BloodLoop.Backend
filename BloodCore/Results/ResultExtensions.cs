using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodCore.Results;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;

namespace BloodCore.Common
{
    public static class ResultExtensions
    {
        public static ActionResult<TValue> ToActionResult<TValue>(this Either<Error, TValue> result)
            => result.Match(
                Right: success => ToSuccessResult(success, value => value),
                Left: error => ToErrorResult<TValue>(error));

        public static ActionResult<TModel> ToActionResult<TValue, TModel>(
            this Either<Error, TValue> result,
            Func<TValue, TModel> valueMapper)
            => result.Match(
                Right: success => ToSuccessResult(success, valueMapper),
                Left: error => ToErrorResult<TModel>(error));

        public static ActionResult ToUntypedActionResult<TValue>(
            this Either<Error, TValue> result,
            Func<TValue, ActionResult> successMapper)
            => result.Match(
                Right: successMapper,
                Left: error => ToErrorResult(error));

        private static ActionResult<TModel> ToSuccessResult<TValue, TModel>(
            TValue result,
            Func<TValue, TModel> valueMapper)
            => result is MediatR.Unit
                ? (ActionResult<TModel>)new NoContentResult()
                : valueMapper(result);

        private static ActionResult<TModel> ToErrorResult<TModel>(Error error)
            => error.Accept<ErrorMappingVisitor<TModel>, ActionResult<TModel>>(new ErrorMappingVisitor<TModel>());

        private static ActionResult ToErrorResult(Error error)
            => error.Accept<ErrorMappingVisitor<object>, ActionResult<object>>(new ErrorMappingVisitor<object>()).Result;
    }
}
