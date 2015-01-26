using FluentValidation.Results;
using NerveFramework.Core.Transactions;

namespace NerveFramework.Core.Validation
{
    /// <summary>
    /// Processes validation
    /// </summary>
    public interface IProcessValidation
    {
        /// <summary>
        /// Validates a command
        /// </summary>
        /// <param name="command">The command which should be validated</param>
        /// <returns>The <see cref="ValidationResult"/> of the command</returns>
        ValidationResult Validate(ICommand command);

        /// <summary>
        /// Validates a query
        /// </summary>
        /// <typeparam name="TResult">The query which could be validated</typeparam>
        /// <param name="query">The query constructor arguments</param>
        /// <returns>The <see cref="ValidationResult"/> of the query</returns>
        ValidationResult Validate<TResult>(IQuery<TResult> query);
    }
}
