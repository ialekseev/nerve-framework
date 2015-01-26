using System.Diagnostics;
using FluentValidation;
using FluentValidation.Results;
using NerveFramework.Core.Transactions;
using NerveFramework.Core.Validation;
using SimpleInjector;

namespace NerveFramework.Infrastructure.FluentValidation
{
    sealed class ValidationProcessor : IProcessValidation
    {
        private readonly Container _container;

        public ValidationProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public ValidationResult Validate<TResult>(IQuery<TResult> query)
        {
            var validatedType = typeof(IValidator<>).MakeGenericType(query.GetType());
            dynamic validator = _container.GetInstance(validatedType);
            return validator.Validate((dynamic)query);
        }

        [DebuggerStepThrough]
        public ValidationResult Validate(ICommand command)
        {
            var validatedType = typeof(IValidator<>).MakeGenericType(command.GetType());
            dynamic validator = _container.GetInstance(validatedType);
            return validator.Validate((dynamic)command);
        }
    }
}
