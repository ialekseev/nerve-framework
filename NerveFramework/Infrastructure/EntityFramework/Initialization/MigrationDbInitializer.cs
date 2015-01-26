using System.Data.Entity;
using NerveFramework.Infrastructure.EntityFramework.Migrations;

namespace NerveFramework.Infrastructure.EntityFramework.Initialization
{
    /// <summary>
    /// Enables migrations on the <see cref="DbContext"/> provided
    /// </summary>
    /// <typeparam name="TContext">The instance of DbContext</typeparam>
    public class MigrationDbInitializer<TContext> : MigrateDatabaseToLatestVersion<TContext, Configuration<TContext>> where TContext : DbContext
    {

    }
}
