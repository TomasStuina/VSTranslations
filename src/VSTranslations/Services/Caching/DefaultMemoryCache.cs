using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using VSTranslations.Extensions;
using VSTranslations.Plugin.Abstractions.Caching;

namespace VSTranslations.Services.Caching;

/// <summary>
/// An in-memory cache implementation of <see cref="ICache"/>
/// that uses <see cref="MemoryCache"/> underneath.
/// </summary>
internal class DefaultMemoryCache : ConfiguredCacheBase
{
    private readonly IMemoryCache _cache;

    public DefaultMemoryCache() : this(CreateInternalMemoryCache())
    {
    }

    internal DefaultMemoryCache(IMemoryCache cache)
    {
        _cache = cache;
    }

    protected override Task<TValue> GetOrCreateValueAsync<TKey, TValue>(TKey key, Func<Task<TValue>> valueFactory) =>
        _cache.GetOrCreateLazyAsync(key, valueFactory);

    private static IMemoryCache CreateInternalMemoryCache()
    {
        var options = Microsoft.Extensions.Options.Options.Create(new MemoryCacheOptions());
        return new MemoryCache(options);
    }
}
