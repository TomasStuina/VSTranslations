using System.Collections.Generic;
using System.Threading.Tasks;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Services.Translating;

/// <summary>
/// Manages <see cref="ITranslatorEngineConfig"/> instance of the current active translator engine.
/// </summary>
internal class TranslatorEngineConfigManager
{
    private readonly Func<Task<ITranslatorEngineProvider>> _translatorEngineProviderFactory;

    internal TranslatorEngineConfigManager(Func<Task<ITranslatorEngineProvider>> translatorEngineProviderFactory)
    {
        _translatorEngineProviderFactory = translatorEngineProviderFactory;
    }

    internal static TranslatorEngineConfigManager Default { get; } = new TranslatorEngineConfigManager(async() => await VS.GetMefServiceAsync<ITranslatorEngineProvider>());

    /// <summary>
    /// Invokes <see cref="ITranslatorEngineConfig.GetLanguagesAsync"/> of the current <see cref="ITranslatorEngineConfig"/>.
    /// </summary>
    /// <returns></returns>
    public async ValueTask<IReadOnlyList<Language>> GetSupportedLanguagesAsync()
    {
        var engineConfig = await GetCurrentConfigAsync();
        if (engineConfig is null)
        {
            return Array.Empty<Language>();
        }

        return await engineConfig.GetLanguagesAsync();
    }

    /// <summary>
    /// Invokes <see cref="ITranslatorEngineConfig.GetSourceLanguageAsync"/> of the current <see cref="ITranslatorEngineConfig"/>.
    /// </summary>
    public async ValueTask<Language> GetSourceLanguageAsync()
    {
        var engineConfig = await GetCurrentConfigAsync();
        if (engineConfig is null)
        {
            return Language.Invariant;
        }

        return await engineConfig.GetSourceLanguageAsync();
    }

    /// <summary>
    /// Invokes <see cref="ITranslatorEngineConfig.GetTargetLanguageAsync"/> of the current <see cref="ITranslatorEngineConfig"/>.
    /// </summary>
    public async ValueTask<Language> GetTargetLanguageAsync()
    {
        var engineConfig = await GetCurrentConfigAsync();
        if (engineConfig is null)
        {
            return Language.Invariant;
        }


        return await engineConfig.GetTargetLanguageAsync();
    }

    /// <summary>
    /// Invokes <see cref="ITranslatorEngineConfig.SetSourceLanguageAsync(Language)"/> of the current <see cref="ITranslatorEngineConfig"/>.
    /// </summary>
    public async ValueTask SetSourceLanguageAsync(Language language)
    {
        var engineConfig = await GetCurrentConfigAsync();
        if (engineConfig is null)
        {
            return;
        }

        await engineConfig.SetSourceLanguageAsync(language);
    }

    /// <summary>
    /// Invokes <see cref="ITranslatorEngineConfig.SetTargetLanguageAsync(Language)"/> of the current <see cref="ITranslatorEngineConfig"/>.
    /// </summary>
    public async ValueTask SetTargetLanguageAsync(Language language)
    {
        var engineConfig = await GetCurrentConfigAsync();
        if (engineConfig is null)
        {
            return;
        }

        await engineConfig.SetTargetLanguageAsync(language);
    }

    /// <summary>
    /// Gets the current active <see cref="ITranslatorEngineConfig"/>.
    /// </summary>
    /// <returns></returns>
    public async Task<ITranslatorEngineConfig> GetCurrentConfigAsync()
    {
        var engineProvider = await _translatorEngineProviderFactory();
        var engine = await engineProvider.GetAsync();
        return engine?.TranslatorEngineConfig;
    }
}
