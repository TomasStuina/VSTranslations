using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.Threading;
using System.Threading.Tasks;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Extensions;

/// <summary>
/// Extensions for <see cref="IMemoryCache"/>.
/// </summary>
internal static class MemoryCacheExtensions
{
    /// <summary>
    /// Gets or creates a lazy-asynchronous cache entry
    /// for the provided value.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <param name="memoryCache"><see cref="IMemoryCache"/> instance.</param>
    /// <param name="key">Cache entry key.</param>
    /// <param name="valueFactory">Value factory.</param>
    /// <returns><see cref="Task{TResult}"/> indicating the completion with <typeparamref name="TValue"/> as a result.</returns>
    public static Task<TValue> GetOrCreateLazyAsync<TKey, TValue>(this IMemoryCache memoryCache, TKey key, Func<Task<TValue>> valueFactory)
    {
        var asyncLazyInstance = memoryCache.GetOrCreate(key, _ =>
            new AsyncLazy<TValue>(valueFactory, ThreadHelper.JoinableTaskFactory));

        return asyncLazyInstance.GetValueAsync();
    }
}
