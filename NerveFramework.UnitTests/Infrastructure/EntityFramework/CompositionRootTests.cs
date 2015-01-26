using NerveFramework.Core.Entities;
using NerveFramework.Infrastructure.EntityFramework;
using NerveFramework.Infrastructure.EntityFramework.ModelCreation;
using NerveFramework.UnitTests.Infrastructure.CompositionRoot;
using SimpleInjector;
using Xunit;

namespace NerveFramework.UnitTests.Infrastructure.EntityFramework
{
    public class CompositionRootTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public CompositionRootTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void RegistersEntityWriter_WithScopedLifestyleHybrid_WebRequest_LifetimeScope()
        {
            var registration = _fixture.Container.GetRegistration(typeof(EntityWriter));
            Assert.IsAssignableFrom<ScopedLifestyle>(registration.Lifestyle);
            Assert.Equal("Hybrid Web Request / Lifetime Scope", registration.Lifestyle.Name);
        }

        [Fact]
        public void RegistersIUnitOfWork_WithScopedLifestyleHybrid_WebRequest_LifetimeScope()
        {
            var registration = _fixture.Container.GetRegistration(typeof(IUnitOfWork));
            Assert.IsAssignableFrom<ScopedLifestyle>(registration.Lifestyle);
            Assert.Equal("Hybrid Web Request / Lifetime Scope", registration.Lifestyle.Name);
        }

        [Fact]
        public void RegistersIWriteEntities_WithScopedLifestyleHybrid_WebRequest_LifetimeScope()
        {
            var registration = _fixture.Container.GetRegistration(typeof(IWriteEntities));
            Assert.IsAssignableFrom<ScopedLifestyle>(registration.Lifestyle);
            Assert.Equal("Hybrid Web Request / Lifetime Scope", registration.Lifestyle.Name);
        }

        [Fact]
        public void RegistersIReadEntities_WithScopedLifestyleHybrid_WebRequest_LifetimeScope()
        {
            var registration = _fixture.Container.GetRegistration(typeof(IReadEntities));
            Assert.IsAssignableFrom<ScopedLifestyle>(registration.Lifestyle);
            Assert.Equal("Hybrid Web Request / Lifetime Scope", registration.Lifestyle.Name);
        }

        [Fact]
        public void RegistersInterfaceImplementations_AsSameInstances()
        {
            using (_fixture.Container.BeginLifetimeScope())
            {
                var entityWriter = _fixture.Container.GetInstance<EntityWriter>();
                var unitOfWork = _fixture.Container.GetInstance<IUnitOfWork>();
                var commands = _fixture.Container.GetInstance<IWriteEntities>();
                var queries = _fixture.Container.GetInstance<IReadEntities>();

                Assert.Equal(entityWriter, unitOfWork);
                Assert.Equal(entityWriter, commands);
                Assert.Equal(entityWriter, queries);
            }
        }

        [Fact]
        public void RegistersICreateDbModel_UsingDefaulDbtModelCreator_Transiently()
        {
            var instance = _fixture.Container.GetInstance<ICreateDbModel>();
            var registration = _fixture.Container.GetRegistration(typeof(ICreateDbModel));

            Assert.NotNull(instance);
            Assert.IsType<DefaultDbModelCreator>(instance);
            Assert.Equal(Lifestyle.Transient, registration.Lifestyle);
        }
    }
}
