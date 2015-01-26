using System.Threading.Tasks;
using NerveFramework.Core.Events;

namespace NerveFramework.UnitTests.Infrastructure.Events.Fakes
{
    public class FakeEventWithoutValidation : IEvent
    {
        public string ReturnValue { get; internal set; }
    }

    public class HandleFakeEventWithoutValidation : IHandleEvent<FakeEventWithoutValidation>
    {
        public Task Handle(FakeEventWithoutValidation @event)
        {
            @event.ReturnValue = "faked";
            return Task.FromResult(0);
        }
    }
}
