using FluentValidation.Mvc;
using NerveFramework.Infrastructure.FluentValidation;
using SimpleInjector;

namespace NerveFramework.Web.Mvc
{
    /// <summary>
    /// Simple Injector Container extensions
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Register Fluent Validation in the Simple Injector pipeline
        /// </summary>
        /// <param name="container">The Simple Injector container</param>
        /// <param name="implicitRequiredValidator">If validators are required (default false)</param>
        public static void RegisterNerveFluentValidationMvc(this Container container, bool implicitRequiredValidator = false)
        {
            FluentValidationModelValidatorProvider.Configure(provider => {
                    provider.ValidatorFactory = new ValidatorFactory(container);
                    provider.AddImplicitRequiredValidator = implicitRequiredValidator;
                }
            );
        }
    }
}
