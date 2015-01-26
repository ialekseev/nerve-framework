using NerveFramework.Core.Events;
using NerveFramework.Infrastructure.CompositionRoot;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NerveFramework.Infrastructure.Events
{
    internal sealed class EventPackage : INervePackage
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            container.RegisterSingle<IProcessEvents, EventProcessor>();
            container.RegisterManyForOpenGeneric(typeof(IHandleEvent<>), container.RegisterAll, settings.EventAssemblies);
            container.RegisterSingleOpenGeneric(typeof(ITriggerEvent<>), typeof(MultipleDispatchEventTrigger<>));
            container.RegisterDecorator(
                typeof(ITriggerEvent<>),
                typeof(TriggerEventWhenHandlersExistDecorator<>)
            );
        }
    }
}
