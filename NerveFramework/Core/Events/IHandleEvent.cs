using System.Threading.Tasks;

namespace NerveFramework.Core.Events
{
    /// <summary>
    /// Handles an <see cref="IEvent"/> implementation
    /// </summary>
    /// <typeparam name="TEvent">An implementation of <see cref="IEvent"/></typeparam>
    public interface IHandleEvent<in TEvent> where TEvent : IEvent
    {
        /// <summary>
        /// The actual 
        /// </summary>
        /// <param name="evt">The <see cref="IEvent"/> passed</param>
        /// <returns></returns>
        Task Handle(TEvent evt);
    }
}
