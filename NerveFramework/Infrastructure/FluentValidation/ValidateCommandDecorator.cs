using System.Diagnostics;
using System.Threading.Tasks;
using FluentValidation;
using NerveFramework.Core.Transactions;

namespace NerveFramework.Infrastructure.FluentValidation
{
    internal class ValidateCommandDecorator<TCommand> : IHandleCommand<TCommand> where TCommand : ICommand
    {
        private readonly IHandleCommand<TCommand> _decorated;
        private readonly IValidator<TCommand> _validator;

        public ValidateCommandDecorator(IHandleCommand<TCommand> decorated
            , IValidator<TCommand> validator
        )
        {
            _decorated = decorated;
            _validator = validator;
        }

        [DebuggerStepThrough]
        public Task Handle(TCommand command)
        {
            _validator.ValidateAndThrow(command);
            return _decorated.Handle(command);
        }
    }
}
