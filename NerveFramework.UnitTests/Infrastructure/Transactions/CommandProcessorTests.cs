using NerveFramework.Core.Transactions;
using NerveFramework.UnitTests.Infrastructure.CompositionRoot;
using NerveFramework.UnitTests.Infrastructure.Transactions.Fakes;
using Xunit;

namespace NerveFramework.UnitTests.Infrastructure.Transactions
{
    public class CommandProcessorTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public CommandProcessorTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Execute_InvokesCommandHandler_UsingContainerForResolution()
        {
            var commands = _fixture.Container.GetInstance<IProcessCommands>();
            var command = new FakeCommandWithoutValidator();
            commands.Execute(command);
          
            Assert.Equal("faked", command.ReturnValue);
        }
    }
}
