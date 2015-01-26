using System.Data.Entity;
using NerveFramework.Infrastructure.EntityFramework.Customization;

namespace NerveFramework.Infrastructure.EntityFramework.Initialization
{
    /// <summary>
    /// A greenfield DbInitializer which drops the database, recreates andoptionally re-seed the database
    /// only if the model has changed since the database was created.
    /// </summary>
    public class GreenfieldDbInitializer : DropCreateDatabaseIfModelChanges<DbContext>
    {
        private readonly ICustomizeDb _customizer;

        /// <summary>
        /// A greenfield DbInitializer which drops the database, recreates andoptionally re-seed the database
        /// only if the model has changed since the database was created.
        /// </summary>
        /// <param name="customizer">The customizer</param>
        public GreenfieldDbInitializer(ICustomizeDb customizer)
        {
            _customizer = customizer;
        }

        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding.
        ///             The default implementation does nothing.
        /// </summary>
        /// <param name="context">The context to seed. </param>
        protected override void Seed(DbContext context)
        {
            if (_customizer != null) _customizer.Customize(context);
        }
    }
}
