using System.Threading.Tasks;
using Moq;
using NerveFramework.Core.Transactions;
using NerveFramework.Infrastructure.Transactions;
using NerveFramework.UnitTests.Infrastructure.CompositionRoot;
using NerveFramework.UnitTests.Infrastructure.Transactions.Fakes;
using SimpleInjector;
using Xunit;

namespace NerveFramework.UnitTests.Infrastructure.Transactions
{
    public class CommandLifetimeScopeDecoratorTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public CommandLifetimeScopeDecoratorTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void BeginsLifetimeScope_WhenCurrentLifetimeScope_IsNull()
        {
            var command = new FakeCommandWithValidator();
            var decorated = new Mock<IHandleCommand<FakeCommandWithValidator>>(MockBehavior.Strict);
            decorated.Setup(x => x.Handle(command)).Returns(Task.FromResult(0));

            var decorator = new CommandLifetimeScopeDecorator<FakeCommandWithValidator>(_fixture.Container, () => decorated.Object);
            Assert.Equal(null, _fixture.Container.GetCurrentLifetimeScope());
            decorator.Handle(command);

            Assert.Equal(null, _fixture.Container.GetCurrentLifetimeScope());
            decorated.Verify(x => x.Handle(command), Times.Once);
        }

        [Fact]
        public void UsesCurrentLifetimeScope_WhenCurrentLifetimeScope_IsNotNull()
        {
            var command = new FakeCommandWithValidator();
            var decorated = new Mock<IHandleCommand<FakeCommandWithValidator>>(MockBehavior.Strict);
            decorated.Setup(x => x.Handle(command)).Returns(Task.FromResult(0));

            var decorator = new CommandLifetimeScopeDecorator<FakeCommandWithValidator>(_fixture.Container, () => decorated.Object);
            Assert.Equal(null, _fixture.Container.GetCurrentLifetimeScope());

            using (_fixture.Container.BeginLifetimeScope())
            {
                decorator.Handle(command);
            }

            Assert.Equal(null, _fixture.Container.GetCurrentLifetimeScope());

            decorated.Verify(x => x.Handle(command), Times.Once);
        }
    }
}
