using System;
using NerveFramework.Web.WebApi;
using Xunit;

namespace NerveFramework.UnitTests.Infrastructure.CompositionRoot
{
    public class WebApiDependencyResolverTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public WebApiDependencyResolverTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Ctor_ThrowsArgumentNullException_WhenContainerArgIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new WebApiDependencyResolver(null));
            Assert.NotNull(exception);
            Assert.Equal("container", exception.ParamName);
        }

        [Fact]
        public void BeginScope_ReturnsSameInstanceOfResolver()
        {
            var resolver = new WebApiDependencyResolver(_fixture.Container);
            var scope = resolver.BeginScope();
            Assert.Equal(resolver, scope);
        }

        [Fact]
        public void Dispose_IsImplemented_AsNoOp()
        {
            var resolver = new WebApiDependencyResolver(_fixture.Container);
            resolver.Dispose();
            Assert.NotNull(resolver);
        }
    }
}
