using NerveFramework.Core.Transactions;
using NerveFramework.UnitTests.Infrastructure.CompositionRoot;
using NerveFramework.UnitTests.Infrastructure.Transactions.Fakes;
using Xunit;

namespace NerveFramework.UnitTests.Infrastructure.Transactions
{
    public class QueryProcessorTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public QueryProcessorTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Execute_InvokesQueryHandler_UsingContainerForResolution()
        {
            var queries = _fixture.Container.GetInstance<IProcessQueries>();
            var result = queries.Execute(new FakeQueryWithoutValidator
            {
                ReturnValue = "faked"
            });

            Assert.Equal("faked", result);
        }
    }
}
