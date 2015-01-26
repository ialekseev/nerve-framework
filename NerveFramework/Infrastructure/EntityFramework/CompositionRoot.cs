using System.Data.Entity;
using NerveFramework.Core.Entities;
using NerveFramework.Infrastructure.EntityFramework.ModelCreation;
using SimpleInjector;

namespace NerveFramework.Infrastructure.EntityFramework
{
    /// <summary>
    /// Composition Root extensions
    /// </summary>
    public static class CompositionRoot
    {
        /// <summary>
        /// Registers the Entity Framework in the Simple Injector pipeline
        /// </summary>
        /// <typeparam name="TContext">The Entity Framework DbContext</typeparam>
        /// <param name="container">The Simple Injector Container</param>
        /// <param name="lifestyle">A lifestyle of the DbContext registration</param>
        public static void RegisterEntityFramework<TContext>(this Container container, ScopedLifestyle lifestyle) where TContext : DbContext
        {
            container.Register<ICreateDbModel, DefaultDbModelCreator>();
            container.Register<DbContext, TContext>(lifestyle);

            var registration = lifestyle.CreateRegistration(typeof(EntityWriter), container);
            container.AddRegistration(typeof(EntityWriter), registration);
            container.AddRegistration(typeof(IUnitOfWork), registration);
            container.AddRegistration(typeof(IWriteEntities), registration);
            container.AddRegistration(typeof(IReadEntities), registration);
        }
    }
}
