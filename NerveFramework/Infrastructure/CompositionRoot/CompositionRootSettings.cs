using System.Reflection;
using NerveFramework.Core.Events;
using NerveFramework.Core.Transactions;

namespace NerveFramework.Infrastructure.CompositionRoot
{
    /// <summary>
    /// The main composition settings. Nerve Framework uses the assembly paths
    /// to wire up the functionality of the application.
    /// </summary>
    public class CompositionRootSettings
    {
        /// <summary>
        /// Where the <see cref="StructuralTypeConfiguration{TStructuralType}"/> are located.
        /// </summary>
        public Assembly[] DatabaseSchemeAssemblies { get; set; }

        /// <summary>
        /// Where the <see cref="IHandleCommand{TCommand}"/> and <see cref="IHandleQuery{TQuery,TResult}"/> are located.
        /// </summary>
        public Assembly[] TransactionAssemblies { get; set; }

        /// <summary>
        /// Where the <see cref="IHandleEvent{TEvent}"/> are located.
        /// </summary>
        public Assembly[] EventAssemblies { get; set; }

        /// <summary>
        /// Where the <see cref="AbstractValidator{T}"/> are located
        /// </summary>
        public Assembly[] FluentValidationAssemblies { get; set; }
    }
}
