namespace NerveFramework.Core.Events
{
    /// <summary>
    /// Processes events by delegating to the implemented handler of the <see cref="IEvent"/>
    /// </summary>
    public interface IProcessEvents
    {
        /// <summary>
        /// Raises the correspondent <see cref="IHandleEvent{IEvent}"/> implementation
        /// </summary>
        /// <param name="evt">The event which should be raised</param>
        /// <returns>An async Task</returns>
        void Raise(IEvent evt);
    }
}
