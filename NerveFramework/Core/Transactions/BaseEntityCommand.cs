using NerveFramework.Core.Events;

namespace NerveFramework.Core.Transactions
{
    /// <summary>
    /// Defines if the command is an entity command.  Used with <see cref="ICommand"/> or an <see cref="ISecuredCommand"/>
    /// </summary>
    public abstract class BaseEntityCommand : IEvent
    {
        /// <summary>
        /// Sets up the command
        /// </summary>
        protected BaseEntityCommand()
        {
            Commit = true;
        }

        /// <summary>
        /// Should the transaction be committed. Default value is true
        /// </summary>
        public bool Commit { get; set; }
    }
}
