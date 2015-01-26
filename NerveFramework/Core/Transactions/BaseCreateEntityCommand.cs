using NerveFramework.Core.Entities;

namespace NerveFramework.Core.Transactions
{
    /// <summary>
    /// An abstract base command for creating an entitiy. Gives access to <see cref="CreatedEntity"/> for returning the newly created entity.
    /// </summary>
    /// <typeparam name="TEntity">The entity which should be created</typeparam>
    public abstract class BaseCreateEntityCommand<TEntity> : BaseEntityCommand where TEntity : Entity
    {
        /// <summary>
        /// The created entity of the command
        /// </summary>
        public TEntity CreatedEntity { get; set; }
    }
}