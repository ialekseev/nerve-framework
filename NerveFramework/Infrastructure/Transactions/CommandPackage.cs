using NerveFramework.Core.Transactions;
using NerveFramework.Infrastructure.CompositionRoot;
using NerveFramework.Infrastructure.FluentValidation;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NerveFramework.Infrastructure.Transactions
{
    internal sealed class CommandPackage : INervePackage
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            container.RegisterSingle<IProcessCommands, CommandProcessor>();
            container.RegisterManyForOpenGeneric(typeof(IHandleCommand<>), settings.TransactionAssemblies);

            container.RegisterDecorator(
                typeof(IHandleCommand<>),
                typeof(CommandedEventProcessingDecorator<>)
            );

            container.RegisterDecorator(
               typeof(IHandleCommand<>),
               typeof(ValidateCommandDecorator<>)
            );
            container.RegisterSingleDecorator(
                typeof(IHandleCommand<>),
                typeof(CommandLifetimeScopeDecorator<>)
            );
            container.RegisterSingleDecorator(
                typeof(IHandleCommand<>),
                typeof(CommandNotNullDecorator<>)
            );
        }
    }
}
