using System.Collections.Generic;
using System.Linq;
using NerveFramework.Core.Events;
using SimpleInjector;

namespace NerveFramework.Infrastructure.Events
{
    /// <summary>
    /// Fires multiple dispatch event triggers
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    internal sealed class MultipleDispatchEventTrigger<TEvent> : ITriggerEvent<TEvent> where TEvent : IEvent
    {
        private readonly Container _container;

        public MultipleDispatchEventTrigger(Container container)
        {
            _container = container;
        }

        public void Trigger(TEvent e)
        {
            var handlers = GetHandlers(_container);
            if (handlers == null || !handlers.Any()) return;

            foreach (var handler in handlers)
            {
                handler.Handle(e);
            }
        }

        internal static IList<IHandleEvent<TEvent>> GetHandlers(Container container)
        {
            var handlersType = typeof(IEnumerable<IHandleEvent<TEvent>>);
            var handlers = container.GetCurrentRegistrations()
                .Where(x => handlersType.IsAssignableFrom(x.ServiceType))
                .Select(x => x.GetInstance()).Cast<IEnumerable<IHandleEvent<TEvent>>>()
                .SelectMany(x =>
                {
                    var handleEvents = x as IHandleEvent<TEvent>[] ?? x.ToArray();
                    return handleEvents;
                })
                .ToArray()
            ;
            return handlers;
        }
    }
}
