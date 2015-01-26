using System.Threading.Tasks;

namespace NerveFramework.Core.Transactions
{
    /// <summary>
    /// Responsible of handling a command
    /// </summary>
    /// <typeparam name="TCommand">The class type of the command</typeparam>
    public interface IHandleCommand<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Handles a command
        /// </summary>
        /// <param name="command">The command</param>
        /// <returns>An awaitable task</returns>
        Task Handle(TCommand command);
    }
}