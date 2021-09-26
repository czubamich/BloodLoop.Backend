using BloodCore.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodCore.Common
{
    public readonly struct ErrorMappingVisitor<TModel> : Error.IErrorVisitor<ActionResult<TModel>>
    {
        public ActionResult<TModel> Visit(Error.NotFound result)
            => new NotFoundObjectResult(ErrorResponse.FromMessage(StatusCodes.Status404NotFound, "Not Found", result.Message));

        public ActionResult<TModel> Visit(Error.Invalid result)
            => new BadRequestObjectResult(ErrorResponse.FromMessage(StatusCodes.Status400BadRequest, "Bad Request", result.Message));

        public ActionResult<TModel> Visit(Error.Unauthorized result)
            => new UnauthorizedObjectResult(ErrorResponse.FromMessage(StatusCodes.Status401Unauthorized, "Unauthorized", result.Message));

        public ActionResult<TModel> Visit(Error.Validation result)
            => new BadRequestObjectResult(ErrorResponse.FromDictionary(StatusCodes.Status401Unauthorized, "One or more validation errors occurred", result.Errors));
    }
}