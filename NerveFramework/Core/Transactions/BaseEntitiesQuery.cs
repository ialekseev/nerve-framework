using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NerveFramework.Core.Entities;

namespace NerveFramework.Core.Transactions
{ 
    /// <summary>
    /// An abstract base entities query which works with queries that return more than one row.
    /// Useful for sorting and eager loading
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseEntitiesQuery<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Eager load configuration the entity
        /// </summary>
        public IEnumerable<Expression<Func<TEntity, object>>> EagerLoad { get; set; }

        /// <summary>
        /// Order the entity
        /// </summary>
        public IDictionary<Expression<Func<TEntity, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
