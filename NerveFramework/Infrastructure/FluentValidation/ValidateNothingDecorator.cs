using FluentValidation;

namespace NerveFramework.Infrastructure.FluentValidation
{
    /// <summary>
    /// Adds unregistered type resolution for objects missing an IValidator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ValidateNothingDecorator<T> : AbstractValidator<T>
    {
      
    }
}
