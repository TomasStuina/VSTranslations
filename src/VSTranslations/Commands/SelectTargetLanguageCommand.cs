using System.Collections.Generic;
using System.Threading.Tasks;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;

namespace VSTranslations.Commands
{
    [Command(PackageGuids.VSTranslationsString, PackageIds.VSTranslations_TranslationToolbarGroup_ToIndexCombo)]
    public class SelectTargetLanguageCommand : DynamicSelectCommandBase<SelectTargetLanguageCommand, Language>
    {
        protected override string DisplayText(Language item) => item.Name;

        protected override async Task<IReadOnlyList<Language>> InitializeItemsAsync() =>
            await TranslatorEngineConfigManager.Default.GetSupportedLanguagesAsync();

        protected override async Task OnItemSelectedAsync(int index, Language language) =>
            await TranslatorEngineConfigManager.Default.SetTargetLanguageAsync(language);

        protected override async Task InitializeCompletedAsync()
        {
            var targetLanguage = await TranslatorEngineConfigManager.Default.GetTargetLanguageAsync();
            await SelectItemAsync(language => language.Code == targetLanguage.Code);
        }

        [Command(PackageGuids.VSTranslationsString, PackageIds.VSTranslations_TranslationToolbarGroup_ToIndexComboGetList)]
        public class ListTargetLanguageCommand : DynamicListCommandBase<ListTargetLanguageCommand, Language>
        {
            protected override string DisplayText(Language item) => item.Name;

            protected override async Task<IReadOnlyList<Language>> InitializeItemsAsync() =>
                await TranslatorEngineConfigManager.Default.GetSupportedLanguagesAsync();
        }
    }
}
