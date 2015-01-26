using System.Linq;
using NerveFramework.Core.Transactions;
using NerveFramework.Infrastructure.FluentValidation;
using NerveFramework.Infrastructure.Transactions;
using NerveFramework.UnitTests.Infrastructure.CompositionRoot;
using NerveFramework.UnitTests.Infrastructure.Transactions.Fakes;
using SimpleInjector;
using Xunit;

namespace NerveFramework.UnitTests.Infrastructure.Transactions
{
    public class CompositionRootTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public CompositionRootTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void RegistersIProcessQueries_UsingQueryProcessor_AsSingleton()
        {
            var instance = _fixture.Container.GetInstance<IProcessQueries>();
            var registration = _fixture.Container.GetRegistration(typeof (IProcessQueries));

            Assert.NotNull(instance);
            Assert.IsType<QueryProcessor>(instance);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);
        }

        [Fact(Skip = "http://stackoverflow.com/questions/27668782/how-to-unit-test-open-generic-decorator-chains-in-simpleinjector-2-6-1")] 
        public void RegistersIHandleQuery_UsingOpenGenerics_WithDecorationChain()
        {
            var instance = _fixture.Container.GetInstance<IHandleQuery<FakeQueryWithoutValidator, string>>();
            Assert.NotNull(instance);

            var registration = (
                from currentRegistration in _fixture.Container.GetCurrentRegistrations()
                where currentRegistration.ServiceType ==
                    typeof(IHandleQuery<FakeQueryWithoutValidator, string>)
                select currentRegistration.Registration)
                .Single();

            Assert.Equal(
                typeof(QueryNotNullDecorator<FakeQueryWithoutValidator, string>),
                registration.ImplementationType);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);

            registration = registration.GetRelationships().Single().Dependency.Registration;
            Assert.Equal(
                typeof(QueryLifetimeScopeDecorator<FakeQueryWithoutValidator, string>),
                registration.ImplementationType);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);

            registration = registration.GetRelationships().Single().Dependency.Registration;
            Assert.Equal(
                typeof(ValidateQueryDecorator<FakeQueryWithoutValidator, string>),
                registration.ImplementationType);
            Assert.Equal(Lifestyle.Transient, registration.Lifestyle);
        }

        [Fact]
        public void RegistersIProcessCommands_UsingCommandProcessor_AsSingleton()
        {
            var instance = _fixture.Container.GetInstance<IProcessCommands>();
            var registration = _fixture.Container.GetRegistration(typeof (IProcessCommands));

            Assert.NotNull(instance);
            Assert.IsType<CommandProcessor>(instance);
            Assert.Equal(Lifestyle.Singleton, registration.Lifestyle);
        }

        [Fact(Skip = "http://stackoverflow.com/questions/27668782/how-to-unit-test-open-generic-decorator-chains-in-simpleinjector-2-6-1")] 
        public void RegistersIHandleCommand_UsingOpenGenerics_WithDecorationChain()
        {
            var instance = _fixture.Container.GetInstance<IHandleCommand<FakeCommandWithValidator>>();
            var registration = _fixture.Container.GetRegistration(typeof (IHandleCommand<FakeCommandWithValidator>));

            Assert.NotNull(instance);
            Assert.IsType<HandleFakeCommandWithValidator>(registration.Registration.ImplementationType);
            Assert.Equal(Lifestyle.Transient, registration.Registration.Lifestyle);

            var decoratorChain = registration.GetRelationships()
                .Select(x => new
                {
                    x.ImplementationType,
                    x.Lifestyle,
                })
                .Reverse().Distinct().ToArray();

            Assert.Equal(3, decoratorChain.Length);

            Assert.IsType<CommandNotNullDecorator<FakeCommandWithValidator>>(decoratorChain[0].ImplementationType);
            Assert.Equal(Lifestyle.Singleton, decoratorChain[0].Lifestyle);

            Assert.IsType<CommandLifetimeScopeDecorator<FakeCommandWithValidator>>(decoratorChain[1].ImplementationType);
            Assert.Equal(Lifestyle.Singleton, decoratorChain[1].Lifestyle);

            Assert.IsType<ValidateCommandDecorator<FakeCommandWithValidator>>(decoratorChain[2].ImplementationType);
            Assert.Equal(Lifestyle.Transient, decoratorChain[2].Lifestyle);

        }
    }
}
