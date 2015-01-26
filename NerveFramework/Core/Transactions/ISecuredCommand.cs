using System.Security.Principal;

namespace NerveFramework.Core.Transactions
{
    /// <summary>
    /// Defines that the command is relying on a authenticated user
    /// </summary>
    public interface ISecuredCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the principal of the command (most likely the logged in user)
        /// </summary>
        IPrincipal Principal { get; set; }
    }
}