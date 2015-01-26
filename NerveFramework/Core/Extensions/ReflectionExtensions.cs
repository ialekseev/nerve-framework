using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace NerveFramework.Core.Extensions
{
    /// <summary>
    /// Reflection helper extensions
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Test to see if the type is generically assignable from the another type
        /// </summary>
        /// <param name="openGeneric">A class/interface type</param>
        /// <param name="closedGeneric">A class/interface typ</param>
        /// <returns>True if the type is generically assignable from the other type - otherwise false</returns>
        public static bool IsGenericallyAssignableFrom(this Type openGeneric, Type closedGeneric)
        {
            var interfaceTypes = closedGeneric.GetInterfaces();

            if (interfaceTypes.Where(interfaceType => interfaceType.IsGenericType).Any(interfaceType => interfaceType.GetGenericTypeDefinition() == openGeneric))
            {
                return true;
            }

            var baseType = closedGeneric.BaseType;
            if (baseType == null) return false;

            return baseType.IsGenericType &&
                (baseType.GetGenericTypeDefinition() == openGeneric ||
                openGeneric.IsGenericallyAssignableFrom(baseType));
        }

        /// <summary>
        /// Gets the resource text from the assembly
        /// </summary>
        /// <param name="assembly">The assembly you are looking in</param>
        /// <param name="resourceName">The resource name</param>
        /// <returns>A string repesenting the resource text</returns>
        /// <exception cref="InvalidOperationException">Thrown if the stream for the embedded resource could not be set</exception>
        public static string GetManifestResourceText(this Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) throw new InvalidOperationException(string.Format("Unable to get stream for embedded resource '{0}' in assembly '{1}'.", resourceName, assembly.FullName));
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        /// <summary>
        /// Get the embedded resource from the assembly
        /// </summary>
        /// <param name="assembly">The assembly you are looking in</param>
        /// <param name="resourceName">The resource name</param>
        /// <returns>A string representing the resource name</returns>
        public static string GetManifestResourceName(this Assembly assembly, string resourceName)
        {
            var allNames = assembly.GetManifestResourceNames();
            if (allNames.Contains(resourceName))
            {
                return allNames.Single(x => x.Contains(resourceName));
            }
            throw new MissingManifestResourceException(String.Format("The resouce '{0}' could not be found. Are you sure its an embedded resource?", resourceName));
        }
    }
}
