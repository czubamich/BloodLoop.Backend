using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodCore.Persistance
{
    public interface IReadOnlyContext
    {
        IQueryable<T> GetQueryable<T>(params Expression<Func<T, string>>[] includables) where T : class;
    }
}
