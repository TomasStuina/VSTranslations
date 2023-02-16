using System.Collections.Generic;

namespace VSTranslations.Common.Extensions
{
    /// <summary>
    /// <see cref="IDictionary{TKey, TValue}"/> extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Removes values for the provided <paramref name="keys"/>.
        /// </summary>
        /// <typeparam name="TKey">Key type.</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Dictionary to remove from.</param>
        /// <param name="keys">Keys to remove values for.</param>
        public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            dictionary.ThrowIfNull(nameof(dictionary));
            keys.ThrowIfNull(nameof(keys));

            foreach (var key in keys)
            {
                dictionary.Remove(key);
            }
        }
    }
}
