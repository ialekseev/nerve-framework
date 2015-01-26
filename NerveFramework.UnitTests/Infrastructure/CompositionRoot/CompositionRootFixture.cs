using System.Reflection;
using NerveFramework.UnitTests.Infrastructure.EntityFramework.Fakes;
using NerveFramework.Web;
using SimpleInjector;

namespace NerveFramework.UnitTests.Infrastructure.CompositionRoot
{
    public class CompositionRootFixture
    {
        public Container Container { get; private set; }

        public CompositionRootFixture()
        {
            Container = new Container();
            var assemblies = new[] { Assembly.GetExecutingAssembly() };

            Container.RegisterNerveWithDbContext<FakeDbContext>(settings =>
            {
                settings.DatabaseSchemeAssemblies = assemblies;
                settings.EventAssemblies = assemblies;
                settings.FluentValidationAssemblies = assemblies;
                settings.TransactionAssemblies = assemblies;
            });
            
        }

    }

}
