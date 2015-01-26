using FluentValidation;
using NerveFramework.Core.Validation;
using NerveFramework.Infrastructure.CompositionRoot;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NerveFramework.Infrastructure.FluentValidation
{
    internal class ValidationPackage : INervePackage
    {
        public void RegisterServices(Container container, CompositionRootSettings settings)
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            container.RegisterSingle<IProcessValidation, ValidationProcessor>();

            // Fluent Validation open generics
            container.RegisterManyForOpenGeneric(typeof(IValidator<>), settings.FluentValidationAssemblies);

            // Add unregistered type resolution for objects missing an IValidator<T>
            container.RegisterSingleOpenGeneric(typeof(IValidator<>), typeof(ValidateNothingDecorator<>));
        }
    }
}
