using System.Threading.Tasks;
using FluentValidation;
using NerveFramework.Core.Transactions;

namespace NerveFramework.UnitTests.Infrastructure.Transactions.Fakes
{
    public class FakeCommandWithValidator : ICommand
    {
        public string InputValue { get; set; }
        public string ReturnValue { get; internal set; }
    }

    public class ValidateFakeCommandWithValidator : AbstractValidator<FakeCommandWithValidator>
    {
        public ValidateFakeCommandWithValidator()
        {
            RuleFor(x => x.InputValue).NotEmpty();
        }
    }

    public class HandleFakeCommandWithValidator : IHandleCommand<FakeCommandWithValidator>
    {
        public Task Handle(FakeCommandWithValidator command)
        {
            command.ReturnValue = "faked";
            return Task.FromResult(0);
        }
    }
}
