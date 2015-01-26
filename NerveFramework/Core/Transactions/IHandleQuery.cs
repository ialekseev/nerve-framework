namespace NerveFramework.Core.Transactions
{
    /// <summary>
    /// Responsible of handling a query
    /// </summary>
    /// <typeparam name="TQuery">The query</typeparam>
    /// <typeparam name="TResult">The result object of the query</typeparam>
    public interface IHandleQuery<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handles the query
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The result of the query</returns>
        TResult Handle(TQuery query);
    }
}