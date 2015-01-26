using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using NerveFramework.Core.Extensions;
using NerveFramework.Infrastructure.CompositionRoot;

namespace NerveFramework.Infrastructure.EntityFramework.ModelCreation
{
    /// <summary>
    /// The default model creator from Nerve Framework. 
    /// Looks for implementations of the <see cref="StructuralTypeConfiguration{TStructuralType}"/> in the assemblies
    /// defined in the <see cref="CompositionRootSettings.DatabaseSchemeAssemblies"/> of the Nerve Framework
    /// </summary>
    public class DefaultDbModelCreator : ICreateDbModel
    {
        private readonly CompositionRootSettings _settings;

        /// <summary>
        /// The default model creator
        /// </summary>
        /// <param name="settings">The Nerve Framework composition settings</param>
        public DefaultDbModelCreator(CompositionRootSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Creates the model from the 
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        public void Create(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            foreach (var assembly in _settings.DatabaseSchemeAssemblies)
            {
                var typesToRegister =
                    assembly.GetTypes()
                        .Where(
                            t =>
                                !t.IsAbstract &&
                                typeof(StructuralTypeConfiguration<>).IsGenericallyAssignableFrom(t))
                        .ToArray();

                foreach (var configurationInstance in typesToRegister.Select(Activator.CreateInstance))
                {
                    modelBuilder.Configurations.Add((dynamic)configurationInstance);
                }
            }

        }
    }
}
