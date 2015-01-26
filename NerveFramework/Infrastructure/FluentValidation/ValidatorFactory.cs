using System;
using FluentValidation;
using SimpleInjector;

namespace NerveFramework.Infrastructure.FluentValidation
{
    /// <summary>
    /// The Fluent Validation Validator Factory. Used to find instances of
    /// the validator for e.g. commands and queries
    /// </summary>
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private readonly Container _container;

        /// <summary>
        /// The constructor of the factory.
        /// </summary>
        /// <param name="container">The Simple Injector Container</param>
        public ValidatorFactory(Container container)
        {
            _container = container;
        }

        /// <summary>
        /// Creates the instance of the validator
        /// </summary>
        /// <param name="validatorType">What type of validator should be found</param>
        /// <returns>The <see cref="IValidator"/> of the type</returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            return _container.GetInstance(validatorType) as IValidator;
        }
    }
}
