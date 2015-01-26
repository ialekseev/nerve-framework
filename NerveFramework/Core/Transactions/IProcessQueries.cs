using System;

namespace NerveFramework.Core.Transactions
{
    /// <summary>
    /// Executes the handling of queries
    /// </summary>
    public interface IProcessQueries
    {
        /// <summary>
        /// Executes a query command
        /// </summary>
        /// <typeparam name="TResult">The query which should be performed</typeparam>
        /// <param name="query">Query object</param>
        /// <returns>The result of the query</returns>
        /// <exception cref="ArgumentNullException">If the query is null</exception>
        TResult Execute<TResult>(IQuery<TResult> query);
    }
}
