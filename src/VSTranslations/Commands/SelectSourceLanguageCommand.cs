using System.Collections.Generic;
using System.Threading.Tasks;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;

namespace VSTranslations.Commands
{
    [Command(PackageGuids.VSTranslationsString, PackageIds.VSTranslations_TranslationToolbarGroup_FromIndexCombo)]
    public class SelectSourceLanguageCommand : DynamicSelectCommandBase<SelectSourceLanguageCommand, Language>
    {
        protected override string DisplayText(Language item) => item.Name;

        protected override async Task<IReadOnlyList<Language>> InitializeItemsAsync() =>
            await TranslatorEngineConfigManager.Default.GetSupportedLanguagesAsync();

        protected override async Task OnItemSelectedAsync(int index, Language language) =>
            await TranslatorEngineConfigManager.Default.SetSourceLanguageAsync(language);

        protected override async Task InitializeCompletedAsync()
        {
            var sourceLanguage = await TranslatorEngineConfigManager.Default.GetSourceLanguageAsync();
            await SelectItemAsync(language => language.Code == sourceLanguage.Code);
        }

        [Command(PackageGuids.VSTranslationsString, PackageIds.VSTranslations_TranslationToolbarGroup_FromIndexComboGetList)]
        public class ListSourceLanguageCommand : DynamicListCommandBase<ListSourceLanguageCommand, Language>
        {
            protected override string DisplayText(Language item) => item.Name;

            protected override async Task<IReadOnlyList<Language>> InitializeItemsAsync() =>
                await TranslatorEngineConfigManager.Default.GetSupportedLanguagesAsync();
        }
    }
}
