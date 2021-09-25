using Ardalis.GuardClauses;
using BloodCore.Common;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Extensions
{
    public static class GuardExtensions
    {
        public static TIdentity NullOrDefault<TIdentity>(this IGuardClause guardClause, [NotNull] TIdentity input, string parameterName, string? message = null) where TIdentity : Identity
        {
            Guard.Against.Null(input, parameterName);
            if (input.Id.IsDefault())
            {
                throw new ArgumentException(message ?? $"Required input {parameterName} was empty.", parameterName);
            }

            return input;
        }
    }
}
