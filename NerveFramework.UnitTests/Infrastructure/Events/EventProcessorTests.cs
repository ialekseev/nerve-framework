using NerveFramework.Core.Events;
using NerveFramework.UnitTests.Infrastructure.CompositionRoot;
using NerveFramework.UnitTests.Infrastructure.Events.Fakes;
using Xunit;

namespace NerveFramework.UnitTests.Infrastructure.Events
{
    public class EventProcessorTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public EventProcessorTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Execute_InvokesEventHandler_UsingContainerForResolution()
        {
            var events = _fixture.Container.GetInstance<IProcessEvents>();
            var @event = new FakeEventWithoutValidation {
                ReturnValue = "faked"
            };
            events.Raise(@event);

            Assert.Equal("faked", @event.ReturnValue);
        }
    }
}
