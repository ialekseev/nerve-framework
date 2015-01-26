using System.Diagnostics;
using System.Threading.Tasks;
using NerveFramework.Core.Transactions;
using SimpleInjector;

namespace NerveFramework.Infrastructure.Transactions
{
    internal sealed class CommandProcessor : IProcessCommands
    {
        private readonly Container _container;

        public CommandProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public Task Execute(ICommand command)
        {
            var handlerType = typeof(IHandleCommand<>).MakeGenericType(command.GetType());
            dynamic handler = _container.GetInstance(handlerType);
            return handler.Handle((dynamic)command);
        }
    }
}