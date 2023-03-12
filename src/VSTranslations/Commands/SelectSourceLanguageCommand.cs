using System.Collections.Generic;
using System.Threading.Tasks;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;

namespace VSTranslations.Commands
{
    [Command(PackageGuids.VSTranslationsString, PackageIds.VSTranslations_TranslationToolbarGroup_FromIndexCombo)]
    public class SelectSourceLanguageCommand : DynamicSelectCommandBase<SelectSourceLanguageCommand, Language>
    {
        private readonly TranslatorEngineConfigManager _configManager;

        public SelectSourceLanguageCommand() : this(TranslatorEngineConfigManager.Default)
        {

        }

        internal SelectSourceLanguageCommand(TranslatorEngineConfigManager configManager)
        {
            _configManager = configManager;
            _configManager.SourceLanguageChanged = async sourceLanguage =>
                await SelectItemAsync(language => language.Code == sourceLanguage.Code);
        }

        protected override string DisplayText(Language item) => item.Name;

        protected override async Task<IReadOnlyList<Language>> InitializeItemsAsync() =>
            await _configManager.GetSupportedLanguagesAsync();

        protected override async Task OnItemSelectedAsync(int index, Language language) =>
            await _configManager.SetSourceLanguageAsync(language);

        protected override async Task InitializeCompletedAsync()
        {
            var sourceLanguage = await _configManager.GetSourceLanguageAsync();
            await SelectItemAsync(language => language.Code == sourceLanguage.Code);
        }

        [Command(PackageGuids.VSTranslationsString, PackageIds.VSTranslations_TranslationToolbarGroup_FromIndexComboGetList)]
        public class ListSourceLanguageCommand : DynamicListCommandBase<ListSourceLanguageCommand, Language>
        {
            private readonly TranslatorEngineConfigManager _configManager;

            public ListSourceLanguageCommand() : this(TranslatorEngineConfigManager.Default)
            {

            }

            internal ListSourceLanguageCommand(TranslatorEngineConfigManager configManager)
            {
                _configManager = configManager;
            }

            protected override string DisplayText(Language item) => item.Name;

            protected override async Task<IReadOnlyList<Language>> InitializeItemsAsync() =>
                await _configManager.GetSupportedLanguagesAsync();
        }
    }
}
