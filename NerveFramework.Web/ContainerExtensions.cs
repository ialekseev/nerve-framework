using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using NerveFramework.Infrastructure.CompositionRoot;
using NerveFramework.Infrastructure.EntityFramework;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;
using SimpleInjector.Integration.Web;

namespace NerveFramework.Web
{
    /// <summary>
    /// Simple Injector Container Extensions
    /// Used for initializing the Nerve Framework
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Registers the Nerve Framework into the Simple Injector Pipeline
        /// </summary>
        /// <typeparam name="TContext">The DbContext of your application</typeparam>
        /// <param name="container">The Simple Injector Container object</param>
        /// <param name="settings">Nerve Framework configuration object</param>
        public static void RegisterNerveWithDbContext<TContext>(this Container container, Action<CompositionRootSettings> settings) where TContext : DbContext, new()
        {
            if (container == null) 
                throw new ArgumentNullException("container");

           container.RegisterEntityFramework<TContext>(Lifestyle.CreateHybrid(() =>
                HttpContext.Current != null,
                new WebRequestLifestyle(),
                new LifetimeScopeLifestyle()
            ));

            container.RegisterNerve(settings);
        }

        /// <summary>
        /// Registers the Nerve Framework into the Simple Injector Pipeline
        /// </summary>
        /// <param name="container">The Simple Injector Container object</param>
        /// <param name="settings">Nerve Framework configuration object</param>
        public static void RegisterNerve(this Container container, Action<CompositionRootSettings> settings)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            var crs = new CompositionRootSettings();
            settings.Invoke(crs);

            container.Register(() => crs);
            container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);
            container.RegisterNervePackages(crs);
        }

        /// <summary>
        /// Registers the Nerve Framework Packages
        /// </summary>
        /// <param name="container">The Simple Injector Container</param>
        /// <param name="settings">Nerve Framework configuration object</param>
        private static void RegisterNervePackages(this Container container, CompositionRootSettings settings)
        {
            var packages = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                           from type in assembly.GetTypes()
                           where typeof(INervePackage).IsAssignableFrom(type)
                           where !type.IsAbstract
                           select (INervePackage)Activator.CreateInstance(type);

            packages.ToList().ForEach(p => p.RegisterServices(container, settings));
        }
    }
}
