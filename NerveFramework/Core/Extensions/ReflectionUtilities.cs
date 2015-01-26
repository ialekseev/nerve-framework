using System;
using System.Linq;
using System.Reflection;

namespace NerveFramework.Core.Extensions
{
    /// <summary>
    /// Utility class that makes it more easier to manage reflection
    /// </summary>
    public static class ReflectionUtilities
    {
        /// <summary>
        /// Gets a list of assemblies where the class type has references
        /// </summary>
        /// <typeparam name="T">The type which the class should be assignable from</typeparam>
        /// <returns>An enumerable list of assemblies matching the search</returns>
        public static Assembly[] GetAssembliesOf<T>() where T : class
        {
            var assemblies = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where typeof (T).IsAssignableFrom(type)
                where !type.IsAbstract
                select assembly;

            return assemblies.ToArray();
        }

        /// <summary>
        /// Gets the assembly by its complete name
        /// </summary>
        /// <param name="name">The complete assembly name, e.g "Nerve.Data" or "Nerve.Infrastructure" without the .dll extension</param>
        /// <returns>An assembly if found or null if nothing was found</returns>
        public static Assembly GetAssembly(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == name);
        }
    }
}
