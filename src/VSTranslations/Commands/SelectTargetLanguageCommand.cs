using System.Collections.Generic;
using System.Threading.Tasks;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;

namespace VSTranslations.Commands
{
    [Command(PackageGuids.VSTranslationsString, PackageIds.VSTranslations_TranslationToolbarGroup_ToIndexCombo)]
    public class SelectTargetLanguageCommand : DynamicSelectCommandBase<SelectTargetLanguageCommand, Language>
    {
        private readonly TranslatorEngineConfigManager _configManager;

        public SelectTargetLanguageCommand() : this(TranslatorEngineConfigManager.Default)
        {
        }

        internal SelectTargetLanguageCommand(TranslatorEngineConfigManager configManager)
        {
            _configManager = configManager;
            _configManager.TargetLanguageChanged = async targetLanguage =>
                await SelectItemAsync(language => language.Code == targetLanguage.Code);
        }

        protected override string DisplayText(Language item) => item.Name;

        protected override async Task<IReadOnlyList<Language>> InitializeItemsAsync() =>
            await _configManager.GetSupportedLanguagesAsync();

        protected override async Task OnItemSelectedAsync(int index, Language language) =>
            await _configManager.SetTargetLanguageAsync(language);

        protected override async Task InitializeCompletedAsync()
        {
            var targetLanguage = await _configManager.GetTargetLanguageAsync();
            await SelectItemAsync(language => language.Code == targetLanguage.Code);
        }

        [Command(PackageGuids.VSTranslationsString, PackageIds.VSTranslations_TranslationToolbarGroup_ToIndexComboGetList)]
        public class ListTargetLanguageCommand : DynamicListCommandBase<ListTargetLanguageCommand, Language>
        {
            private readonly TranslatorEngineConfigManager _configManager;

            public ListTargetLanguageCommand() : this(TranslatorEngineConfigManager.Default)
            {
            }

            internal ListTargetLanguageCommand(TranslatorEngineConfigManager configManager)
            {
                _configManager = configManager;
            }

            protected override string DisplayText(Language item) => item.Name;

            protected override async Task<IReadOnlyList<Language>> InitializeItemsAsync() =>
                await _configManager.GetSupportedLanguagesAsync();
        }
    }
}
