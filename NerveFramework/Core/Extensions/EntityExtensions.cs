using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NerveFramework.Core.Entities;

namespace NerveFramework.Core.Extensions
{
    /// <summary>
    /// Entity Extensions
    /// </summary>
    public static class EntityExtensions
    {
        #region EagerLoad

        /// <summary>
        /// Eager loads an entity
        /// </summary>
        /// <param name="queryable">The current queryable</param>
        /// <param name="expression">The expression</param>
        /// <typeparam name="TEntity">The <see cref="IQueryable{T}"/> type of the entity</typeparam>
        /// <returns>A chainable <see cref="IQueryable{T}"/></returns>
        public static IQueryable<TEntity> EagerLoad<TEntity>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, object>> expression)
            where TEntity : Entity
        {
            var set = queryable as EntitySet<TEntity>;
            if (set != null)
                set.Queryable = set.Entities.EagerLoad(set.Queryable, expression);
            return queryable;
        }
        /// <summary>
        /// Eager loads an entity
        /// </summary>
        /// <typeparam name="TEntity">The <see cref="IQueryable{T}"/> type of the entity</typeparam>
        /// <param name="queryable">The current queryable</param>
        /// <param name="expressions">The expression</param>
        /// <returns>A chainable <see cref="IQueryable{T}"/></returns>
        public static IQueryable<TEntity> EagerLoad<TEntity>(this IQueryable<TEntity> queryable, IEnumerable<Expression<Func<TEntity, object>>> expressions)
            where TEntity : Entity
        {
            if (expressions != null)
                queryable = expressions.Aggregate(queryable, (current, expression) => current.EagerLoad(expression));
            return queryable;
        }

        #endregion
        #region ById (int)

        /// <summary>
        /// Gets an <see cref="EntityWithId{TId}"/> by its primary (int) key.
        /// </summary>
        /// <param name="set">The <see cref="IQueryable{T}"/></param>
        /// <param name="id">The primary key</param>
        /// <param name="allowNull">If its possible to allow null (default true)</param>
        /// <typeparam name="TEntity">The entity</typeparam>
        /// <returns>The entity matching the condition</returns>
        public static TEntity ById<TEntity>(this IQueryable<TEntity> set, int id, bool allowNull = true) where TEntity : EntityWithId<int>
        {
            return allowNull ? set.SingleOrDefault(ById<TEntity>(id)) : set.Single(ById<TEntity>(id));
        }

        /// <summary>
        /// Gets an <see cref="EntityWithId{TId}"/> by its primary (int) key.
        /// </summary>
        /// <typeparam name="TEntity">The entity</typeparam>
        /// <param name="set">The <see cref="IQueryable{T}"/></param>
        /// <param name="id">>The primary key</param>
        /// <param name="allowNull">If its possible to allow null (default true)</param>
        /// <returns>The entity matching the condition</returns>
        public static TEntity ById<TEntity>(this IEnumerable<TEntity> set, int id, bool allowNull = true) where TEntity : EntityWithId<int>
        {
            return set.AsQueryable().ById(id, allowNull);
        }

        /// <summary>
        /// Gets an <see cref="EntityWithId{TId}"/> by its primary (int) key using a asynchronous call
        /// </summary>
        /// <typeparam name="TEntity">The entity</typeparam>
        /// <param name="set">The <see cref="IQueryable{T}"/></param>
        /// <param name="id">The primary key</param>
        /// <param name="allowNull">If its possible to allow null (default true)</param>
        /// <returns>The entity matching the condition</returns>
        public static Task<TEntity> ByIdAsync<TEntity>(this IQueryable<TEntity> set, int id, bool allowNull = true) where TEntity : EntityWithId<int>
        {
            return allowNull ? set.SingleOrDefaultAsync(ById<TEntity>(id)) : set.SingleAsync(ById<TEntity>(id));
        }

        /// <summary>
        /// Gets an <see cref="EntityWithId{TId}"/> by its primary (int) key using a asynchronous call
        /// </summary>
        /// <typeparam name="TEntity">The entity</typeparam>
        /// <param name="set">The <see cref="IQueryable{T}"/></param>
        /// <param name="id">The primary key</param>
        /// <param name="allowNull">If its possible to allow null (default true)</param>
        /// <returns>The entity matching the condition</returns>
        public static Task<TEntity> ByIdAsync<TEntity>(this IEnumerable<TEntity> set, int id, bool allowNull = true) where TEntity : EntityWithId<int>
        {
            return set.AsQueryable().ByIdAsync(id, allowNull);
        }

        internal static Expression<Func<TEntity, bool>> ById<TEntity>(int id) where TEntity : EntityWithId<int>
        {
            return x => x.Id == id;
        }

        #endregion
    }
}
