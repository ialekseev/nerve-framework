using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NerveFramework.Core.Entities;

namespace NerveFramework.Core.Transactions
{
    /// <summary>
    /// An abstract base entity query that provides extra features such as eager load.
    /// </summary>
    /// <typeparam name="TEntity">The <see cref="Entity"/></typeparam>
    public abstract class BaseEntityQuery<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Eager loads an entity
        /// </summary>
        public IEnumerable<Expression<Func<TEntity, object>>> EagerLoad { get; set; }
    }
}