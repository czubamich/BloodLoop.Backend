using System.Collections.Generic;
using System.Linq;

namespace BloodCore.Results
{
    public abstract class Error
    {
        private Error() { }

        public abstract TResult Accept<TVisitor, TResult>(TVisitor visitor)
            where TVisitor : IErrorVisitor<TResult>;

        public interface IErrorVisitor<out TVisitResult>
        {
            TVisitResult Visit(NotFound result);

            TVisitResult Visit(Invalid result);

            TVisitResult Visit(Unauthorized result);

            TVisitResult Visit(Validation result);
        }

        public sealed class NotFound : Error
        {
            public NotFound(string message)
            {
                Message = message;
            }

            public string Message { get; }


            public override TResult Accept<TVisitor, TResult>(TVisitor visitor)
                => visitor.Visit(this);
        }

        public sealed class Invalid : Error
        {
            public Invalid(string message)
            {
                Message = message;
            }

            public string Message { get; }

            public override TResult Accept<TVisitor, TResult>(TVisitor visitor)
                => visitor.Visit(this);
        }

        public sealed class Validation : Error
        {
            private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

            public IDictionary<string, string[]> Errors => _errors.ToDictionary(k => k.Key, v => v.Value.ToArray());

            public void AddError(string propertyName, string message)
            {
                if (_errors.ContainsKey(propertyName))
                    _errors[propertyName].Add(message);
                else
                    _errors.Add(propertyName, new List<string> { message });
            }

            public override TResult Accept<TVisitor, TResult>(TVisitor visitor)
                => visitor.Visit(this);
        }

        public sealed class Unauthorized : Error
        {
            public Unauthorized(string message)
            {
                Message = message;
            }

            public string Message { get; }

            public override TResult Accept<TVisitor, TResult>(TVisitor visitor)
                => visitor.Visit(this);
        }
    }
}
