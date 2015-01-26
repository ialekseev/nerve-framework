namespace NerveFramework.Core.Events
{
    /// <summary>
    /// Triggers event
    /// </summary>
    /// <typeparam name="TEvent">The event to be triggered</typeparam>
    public interface ITriggerEvent<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// Triggers the correspondent <see cref="IHandleEvent{IEvent}"/> implementation
        /// </summary>
        /// <param name="evt">The event which should be triggered</param>
        void Trigger(TEvent evt);
    }
}
