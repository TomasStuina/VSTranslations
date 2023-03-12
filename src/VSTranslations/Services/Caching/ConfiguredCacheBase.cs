using System.Threading.Tasks;
using VSTranslations.Options;
using VSTranslations.Plugin.Abstractions.Caching;

namespace VSTranslations.Services.Caching;

/// <summary>
/// A base class implementing <see cref="ICache"/> that
/// provides controlled caching via <see cref="GeneralOptions"/>.
/// </summary>
internal abstract class ConfiguredCacheBase : ICache
{
    /// <inheritdoc/>
    public async Task<TValue> GetOrCreateAsync<TKey, TValue>(TKey key, Func<Task<TValue>> valueFactory)
    {
        var options = await GeneralOptions.GetLiveInstanceAsync();
        if (options is null || !options.EnableCaching)
        {
            return await valueFactory.Invoke();
        }

        return await GetOrCreateValueAsync(key, valueFactory);
    }

    protected abstract Task<TValue> GetOrCreateValueAsync<TKey, TValue>(TKey key, Func<Task<TValue>> valueFactory);
}
