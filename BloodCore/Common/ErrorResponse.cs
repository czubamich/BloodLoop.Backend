using System.Collections.Generic;

namespace BloodCore.Common
{
    public class ErrorResponse<T> where T : class
    {
        public int StatusCode { get; init; }
        public string Topic { get; init; }
        public T Errors { get; init; }

        public ErrorResponse(int statusCode, string message, T errors)
        {
            StatusCode = statusCode;
            Topic = message;
            Errors = errors;
        }
    }

    public static class ErrorResponse
    {
        public static ErrorResponse<IDictionary<string, string[]>> FromDictionary(int statusCode, string message, IDictionary<string, string[]> errorDictionary)
            => new(statusCode, message, errorDictionary);

        public static ErrorResponse<IEnumerable<string>> FromList(int statusCode, string message, IEnumerable<string> errors)
            => new(statusCode, message, errors);

        public static ErrorResponse<string> FromMessage(int statusCode, string message, string error)
            => new(statusCode, message, error);
    }
}
