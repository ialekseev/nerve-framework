using System;
using System.Web.Mvc;
using NerveFramework.Core.Transactions;

namespace NerveFramework.Web.Mvc
{
    /// <summary>
    /// Setup the default model binder configuration for the Nerve Framework
    /// </summary>
    public static class ModelConfig
    {
        /// <summary>
        /// Configures the default model binder to auto bind <see cref="ISecuredCommand">ISecuredCommand</see> principal to the current logged in user.
        /// </summary>
        public static void Configure()
        {
            ModelBinders.Binders.DefaultBinder = new SecuredCommandModelBinder();
        }
    }

    /// <summary>
    /// This auto-binds the principal to all secured commands
    /// </summary>
    public class SecuredCommandModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// Creates the specified model type by using the specified controller context and binding context.
        /// </summary>
        /// <returns>
        /// A data object of the specified type.
        /// </returns>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param><param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param><param name="modelType">The type of the model object to return.</param>
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            var baseModel = base.CreateModel(controllerContext, bindingContext, modelType);
            var commandModel = baseModel as ISecuredCommand;
            if (commandModel != null) commandModel.Principal = controllerContext.HttpContext.User;
            return baseModel;
        }
    }
}
