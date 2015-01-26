using System.Threading.Tasks;

namespace NerveFramework.Core.Transactions
{
    /// <summary>
    /// Executes the handling of commands
    /// </summary>
    public interface IProcessCommands
    {
        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="command">The command object</param>
        Task Execute(ICommand command);
    }
}
