using Microsoft.VisualStudio.Threading;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Plugin.GoogleTranslate;

[Export(typeof(ITranslatorEngineConfig<GoogleTranslatorEngine>))]
public class GoogleTranslatorEngineConfig : ITranslatorEngineConfig<GoogleTranslatorEngine>
{
    private readonly Dictionary<string, Language> _languagesByCode;

    public GoogleTranslatorEngineConfig()
    {
        _languagesByCode = SupportedLanguages.Languages.ToDictionary(
            language => language.Code,
            language => language,
            StringComparer.OrdinalIgnoreCase);
    }

    public ValueTask<IReadOnlyList<Language>> GetLanguagesAsync() => new(SupportedLanguages.Languages);

    public async ValueTask<Language> GetSourceLanguageAsync()
    {
        var options = await GoogleTranslatorEngineOptions.GetLiveInstanceAsync();
        return GetLanguageByCodeOrDefault(options.SourceLanguageCode, LanguageDirection.Source);
    }

    public async ValueTask<Language> GetTargetLanguageAsync()
    {
        var options = await GoogleTranslatorEngineOptions.GetLiveInstanceAsync();
        return GetLanguageByCodeOrDefault(options.TargetLanguageCode, LanguageDirection.Target);
    }

    public async ValueTask SetSourceLanguageAsync(Language language)
    {
        var options = await GoogleTranslatorEngineOptions.GetLiveInstanceAsync();
        options.SourceLanguageCode = language.Code;

        await options.SaveAsync();
    }

    public async ValueTask SetTargetLanguageAsync(Language language)
    {
        var options = await GoogleTranslatorEngineOptions.GetLiveInstanceAsync();
        options.TargetLanguageCode = language.Code;

        await options.SaveAsync();
    }

    private Language GetLanguageByCodeOrDefault(string languageCode, LanguageDirection direction)
    {
        return languageCode is not null && _languagesByCode.TryGetValue(languageCode, out var language)
            ? language
            : GetFirstLanguageOrDefault(direction);
    }

    private Language GetFirstLanguageOrDefault(LanguageDirection direction)
    {
        return _languagesByCode.Values.FirstOrDefault(language => language.Direction.HasFlag(direction)) ?? Language.Invariant;
    }

    public class GoogleTranslatorEngineOptions : BaseOptionModel<GoogleTranslatorEngineOptions>
    {
        public string SourceLanguageCode { get; set; }

        public string TargetLanguageCode { get; set; }
    }
}
