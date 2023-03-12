using System.Threading.Tasks;
using System;

namespace VSTranslations.Plugin.Abstractions.Caching;

/// <summary>
/// An interface describing cache service.
/// </summary>
public interface ICache
{
    /// <summary>
    /// Asynchronously gets or creates a cache entry
    /// for the provided value.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="key">Cache entry key.</param>
    /// <param name="valueFactory">Value factory.</param>
    /// <returns><see cref="Task{TResult}"/> indicating the completion with <typeparamref name="TValue"/> as a result.</returns>
    Task<TValue> GetOrCreateAsync<TKey, TValue>(TKey key, Func<Task<TValue>> valueFactory);
}
