using System;

namespace VSTranslations.Common.Extensions
{
    /// <summary>
    /// Generic object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if <paramref name="instance"/>
        /// is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="instance">Instance to check.</param>
        /// <param name="name">Argument name.</param>
        /// <returns>The same <paramref name="instance"/> if not <c>null</c>.</returns>
        public static T ThrowIfNull<T>(this T instance, string name) where T : class
        {
            if (instance is null)
            {
                throw new ArgumentNullException(name);
            }

            return instance;
        }
    }
}
