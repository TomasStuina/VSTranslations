using System.Threading.Tasks;

namespace VSTranslations.Plugin.Abstractions.Translating
{
    /// <summary>
    /// A base class for deriving all translator engines.
    /// </summary>
    /// <remarks>
    /// Recommended to derive from this class instead of directly implementing
    /// <see cref="ITranslatorEngine"/>
    /// </remarks>
    public abstract class TranslatorEngineBase : ITranslatorEngine
    {
        protected TranslatorEngineBase(ITranslatorEngineConfig translatorEngineConfig)
        {
            TranslatorEngineConfig = translatorEngineConfig;
        }

        /// <inheritdoc/>
        public ITranslatorEngineConfig TranslatorEngineConfig { get; }

        /// <inheritdoc/>
        public async Task<string> TranslateAsync(string text)
        {
            var sourceLanguage = await TranslatorEngineConfig.GetSourceLanguageAsync();
            var targetLanguage = await TranslatorEngineConfig.GetTargetLanguageAsync();

            return await TranslateAsync(text, sourceLanguage, targetLanguage);
        }

        protected abstract Task<string> TranslateAsync(string text, Language source, Language destination);
    }
}
