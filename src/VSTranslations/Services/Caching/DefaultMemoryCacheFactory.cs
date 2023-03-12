using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.Composition;
using VSTranslations.Plugin.Abstractions.Caching;

namespace VSTranslations.Services.Caching;

/// <summary>
/// Default implementation of <see cref="ICacheFactory"/>.
/// </summary>
[Export(typeof(ICacheFactory))]
internal class DefaultMemoryCacheFactory : ICacheFactory
{
    /// <summary>
    /// Creates <see cref="DefaultMemoryCache"/> instance if cache is enabled.
    /// </summary>
    /// <returns><see cref="DefaultMemoryCache"/> as <see cref="IMemoryCache"/>.</returns>
    public ICache Create() => new DefaultMemoryCache();
}