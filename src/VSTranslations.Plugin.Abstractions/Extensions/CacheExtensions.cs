using System.Threading.Tasks;
using System;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Caching;

namespace VSTranslations.Plugin.Abstractions.Extensions;

/// <summary>
/// Extensions for <see cref="ICache"/>.
/// </summary>
internal static class CacheExtensions
{
    /// <summary>
    /// Gets or sets translated string for the specific pair
    /// of languages.
    /// </summary>
    /// <param name="cache"><see cref="ICache"/> instance.</param>
    /// <param name="source">Source language.</param>
    /// <param name="sourceText">Source text.</param>
    /// <param name="target">Target language.</param>
    /// <param name="targetTextFactory">Target text factory.</param>
    /// <returns><see cref="Task{TResult}"/> indicating the completion with translated string as a result.</returns>
    public static Task<string> GetOrSetTranslationAsync(this ICache cache, Language source, string sourceText,
        Language target, Func<Task<string>> targetTextFactory)
    {
        var entryKey = $"{source.Code}{target.Code}+{sourceText}";

        return cache.GetOrCreateAsync(entryKey, targetTextFactory);
    }
}