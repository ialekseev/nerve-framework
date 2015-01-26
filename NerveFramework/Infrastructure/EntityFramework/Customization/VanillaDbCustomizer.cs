using System.Data.Entity;

namespace NerveFramework.Infrastructure.EntityFramework.Customization
{
    /// <summary>
    /// A customization of the DbContext, which does absolutely nothing
    /// </summary>
    public class VanillaDbCustomizer : ICustomizeDb
    {
        /// <summary>
        /// Customizes the database context. Used for e.g. running custom SQL-scripts.
        /// </summary>
        /// <param name="context">The context to customize</param>
        public void Customize(DbContext context)
        {
            // Do not customize
        }
    }
}
