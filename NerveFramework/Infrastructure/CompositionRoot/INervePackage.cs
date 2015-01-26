using SimpleInjector;

namespace NerveFramework.Infrastructure.CompositionRoot
{
    /// <summary>
    /// A Nerve Package encapsules the Nerve Framework infrastructure and registeres the services in the Container
    /// </summary>
    public interface INervePackage
    {
        /// <summary>
        /// Registers the services in the <see cref="Container"/>
        /// </summary>
        /// <param name="container">The Simple Injector Container</param>
        /// <param name="settings">The Nerve Framework composition settings</param>
        void RegisterServices(Container container, CompositionRootSettings settings);
    }
}
