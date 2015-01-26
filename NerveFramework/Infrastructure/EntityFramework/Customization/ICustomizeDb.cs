using System.Data.Entity;

namespace NerveFramework.Infrastructure.EntityFramework.Customization
{
    /// <summary>
    /// Customization of the Entity Framework database
    /// </summary>
    public interface ICustomizeDb
    {
        /// <summary>
        /// Customizes the database context. Used for e.g. running custom SQL-scripts.
        /// </summary>
        /// <param name="context">The context to customize</param>
        void Customize(DbContext context);
    }
}
