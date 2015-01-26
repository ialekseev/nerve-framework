using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NerveFramework.Core.Events;
using NerveFramework.Core.Transactions;

namespace NerveFramework.Infrastructure.Transactions
{
    /// <summary>
    /// This will handle the command as normal, and then will try to process it as an event, 
    /// if the command is an instance of a <see cref="BaseEntityCommand"/> that has just been comitted.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    internal sealed class CommandedEventProcessingDecorator<TCommand> : IHandleCommand<TCommand> where TCommand : ICommand
    {
        private readonly IProcessEvents _events;
        private readonly Func<IHandleCommand<TCommand>> _handlerFactory;
        
        public CommandedEventProcessingDecorator(IProcessEvents events, Func<IHandleCommand<TCommand>> handlerFactory)
        {
            _events = events;
            _handlerFactory = handlerFactory;
        }

        [DebuggerStepThrough]
        public Task Handle(TCommand command)
        {
            var handler = _handlerFactory();
            var task = handler.Handle(command);            
            
            var baseEntityCommand = command as BaseEntityCommand;
            if (baseEntityCommand != null && baseEntityCommand.Commit)
            {
                _events.Raise((IEvent)command);
            }

            return task;
        }
    }
}
