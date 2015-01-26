using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NerveFramework.Core.Transactions
{
    /// <summary>
    /// An abstract base class that provides extra features such as order by. 
    /// Useful for querying lists where the ordering/sorting is subject to change at runtime.
    /// Use e.g. when returning a View Model which is not a part of the DbContext
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    public abstract class BaseEnumerableQuery<T>
    {
        /// <summary>
        /// Configure the order
        /// </summary>
        public IDictionary<Expression<Func<T, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
