using System.Data.Entity;

namespace NerveFramework.Infrastructure.EntityFramework.Initialization
{
    /// <summary>
    /// Assume db already exists and should not be fooled with
    /// </summary>
    public class BrownfieldDbInitializer : IDatabaseInitializer<DbContext>
    {
        /// <summary>
        /// Executes the strategy to initialize the database for the given context.
        /// </summary>
        /// <param name="context">The context to initialize</param>
        public void InitializeDatabase(DbContext context)
        {
            
        }
    }
}
