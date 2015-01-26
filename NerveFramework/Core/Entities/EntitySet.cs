using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NerveFramework.Core.Entities
{
    /// <summary>
    /// Works with an <see cref="IQueryable{T}"/>
    /// </summary>
    /// <typeparam name="TEntity">The <see cref="Entity"/></typeparam>
    public class EntitySet<TEntity> : IQueryable<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Works with an <see cref="IQueryable{T}"/>
        /// </summary>
        /// <param name="queryable">The <see cref="IQueryable{T}"/></param>
        /// <param name="entities">The entity reader</param>
        /// <exception cref="ArgumentNullException">If queryable or entities are null</exception>
        public EntitySet(IQueryable<TEntity> queryable, IReadEntities entities)
        {
            if (queryable == null) throw new ArgumentNullException("queryable");
            if (entities == null) throw new ArgumentNullException("entities");
            Queryable = queryable;
            Entities = entities;
        }

        internal IQueryable<TEntity> Queryable { get; set; }
        internal IReadEntities Entities { get; private set; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return Queryable.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.Expressions.Expression"/> that is associated with this instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </returns>
        public Expression Expression { get { return Queryable.Expression; } }

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public Type ElementType { get { return Queryable.ElementType; } }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryProvider"/> that is associated with this data source.
        /// </returns>
        public IQueryProvider Provider { get { return Queryable.Provider; } }
    }
}