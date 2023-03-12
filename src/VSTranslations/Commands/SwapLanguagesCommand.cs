using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;

namespace VSTranslations.Commands;

[Command(PackageGuids.VSTranslationsString, PackageIds.VSTranslations_TranslationToolbarGroup_TranslationSwap)]
public class SwapLanguagesCommand : BaseCommand<SwapLanguagesCommand>
{
    private readonly TranslatorEngineConfigManager _configManager;

    public SwapLanguagesCommand() : this(TranslatorEngineConfigManager.Default)
    {

    }

    internal SwapLanguagesCommand(TranslatorEngineConfigManager configManager)
    {
        _configManager = configManager;
    }

    protected override async Task ExecuteAsync(OleMenuCmdEventArgs _)
    {
        var sourceLanguage = await _configManager.GetSourceLanguageAsync();
        var targetLanguage = await _configManager.GetTargetLanguageAsync();

        if (sourceLanguage is null || targetLanguage is null)
        {
            return;
        }

        if (sourceLanguage.Direction.HasFlag(LanguageDirection.Target)
            && targetLanguage.Direction.HasFlag(LanguageDirection.Source))
        {
            await _configManager.SetSourceLanguageAsync(targetLanguage);
            await _configManager.SetTargetLanguageAsync(sourceLanguage);
        }
    }
}
