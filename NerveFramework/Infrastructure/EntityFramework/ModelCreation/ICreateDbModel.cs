using System.Data.Entity;

namespace NerveFramework.Infrastructure.EntityFramework.ModelCreation
{
    /// <summary>
    /// Creates the database model. Should be called from the <see cref="DbContext.OnModelCreating"/> method of the <see cref="DbContext"/>
    /// </summary>
    public interface ICreateDbModel
    {
        /// <summary>
        /// Creates the model
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        void Create(DbModelBuilder modelBuilder);
    }
}
