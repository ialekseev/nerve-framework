using System.Threading.Tasks;
using NerveFramework.Core.Transactions;

namespace NerveFramework.UnitTests.Infrastructure.Transactions.Fakes
{
    public class FakeCommandWithoutValidator : ICommand
    {
        public string ReturnValue { get; internal set; }
    }

    public class HandleFakeCommandWithoutValidator : IHandleCommand<FakeCommandWithoutValidator>
    {
        public Task Handle(FakeCommandWithoutValidator command)
        {
            command.ReturnValue = "faked";
            return Task.FromResult(0);
        }
    }
}
