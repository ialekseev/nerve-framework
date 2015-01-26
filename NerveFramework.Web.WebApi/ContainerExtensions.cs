using System;
using System.Web.Http;
using NerveFramework.Infrastructure.FluentValidation;
using SimpleInjector;

namespace NerveFramework.Web.WebApi
{
    /// <summary>
    /// Simple Injector Container extensions
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Register Fluent Validation in the Simple Injector pipeline for Web Api
        /// </summary>
        /// <param name="container">The Simple Injector container</param>
        /// <param name="httpConfiguration">The Http Configuration for the request</param>
        public static void RegisterNerveFluentValidationWebApi(this Container container, HttpConfiguration httpConfiguration)
        {
            if (httpConfiguration == null) throw new ArgumentNullException("httpConfiguration");

            FluentValidation.WebApi.FluentValidationModelValidatorProvider.Configure(httpConfiguration, provider =>
            {
                provider.ValidatorFactory = new ValidatorFactory(container);
            });
        }
    }
}
