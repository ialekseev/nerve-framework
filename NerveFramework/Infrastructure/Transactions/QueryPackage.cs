using NerveFramework.Core.Transactions;
using NerveFramework.Infrastructure.CompositionRoot;
using NerveFramework.Infrastructure.FluentValidation;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NerveFramework.Infrastructure.Transactions
{
    internal sealed class QueryPackage : INervePackage
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            container.RegisterSingle<IProcessQueries, QueryProcessor>();
            container.RegisterManyForOpenGeneric(typeof(IHandleQuery<,>), settings.TransactionAssemblies);

            container.RegisterDecorator(
                typeof(IHandleQuery<,>),
                typeof(ValidateQueryDecorator<,>)
            );
            container.RegisterSingleDecorator(
                typeof(IHandleQuery<,>),
                typeof(QueryLifetimeScopeDecorator<,>)
            );
            container.RegisterSingleDecorator(
                typeof(IHandleQuery<,>),
                typeof(QueryNotNullDecorator<,>)
            );
        }
    }
}
