using FluentValidation;
using NerveFramework.Core.Transactions;

namespace NerveFramework.UnitTests.Infrastructure.Transactions.Fakes
{
    public class FakeQueryWithValidator : IQuery<string>
    {
        public string InputValue { get; set; }
    }

    public class ValidateFakeQueryWithValidator: AbstractValidator<FakeQueryWithValidator>
    {
        public ValidateFakeQueryWithValidator()
        {
            RuleFor(x => x.InputValue).NotEmpty();
        }
    }

    public class HandleFakeQueryWithValidator : IHandleQuery<FakeQueryWithValidator, string>
    {
        public string Handle(FakeQueryWithValidator query)
        {
            return "faked";
        }
    }
}
