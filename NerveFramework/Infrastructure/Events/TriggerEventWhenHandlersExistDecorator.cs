using System;
using System.Linq;
using System.Threading.Tasks;
using NerveFramework.Core.Events;
using SimpleInjector;

namespace NerveFramework.Infrastructure.Events
{
    internal sealed class TriggerEventWhenHandlersExistDecorator<TEvent> : ITriggerEvent<TEvent> where TEvent : IEvent
    {
        private readonly Container _container;
        private readonly Func<ITriggerEvent<TEvent>> _factory;

        public TriggerEventWhenHandlersExistDecorator(Container container, Func<ITriggerEvent<TEvent>> factory)
        {
            _container = container;
            _factory = factory;
        }

        public void Trigger(TEvent evt)
        {
            // there is no need to start a new thread unless there are handlers registered for this event
            var handlers = MultipleDispatchEventTrigger<TEvent>.GetHandlers(_container);
            if (handlers != null && handlers.Any())
            {
                Task.Factory.StartNew(() =>
                {
                    if (_container.GetCurrentLifetimeScope() != null)
                    {
                        _factory().Trigger(evt);
                    }
                    else
                    {
                        using (_container.BeginLifetimeScope())
                        {
                            _factory().Trigger(evt);
                        }
                    }
                });
            }
        }
    }
}
