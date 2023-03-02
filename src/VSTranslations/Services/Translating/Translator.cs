using Microsoft.VisualStudio.Text;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Common.Extensions;
using VSTranslations.Extensions;

namespace VSTranslations.Services.Translating
{
    [Export(typeof(ITranslator))]
    internal class Translator : ITranslator
    {
        private readonly ITranslatorEngineProvider _translatorEngineProvider;

        [ImportingConstructor]
        public Translator(ITranslatorEngineProvider translatorEngineProvider)
        {
            _translatorEngineProvider = translatorEngineProvider;
        }

        public async Task<TextLinesCollection> TranslateAsync(SnapshotSpan snapshotSpan)
        {
            var textLinesToTranslate = snapshotSpan.GetLinesCollection();
            var translatorEngine = await _translatorEngineProvider.GetAsync();

            if (translatorEngine is null)
            {
                return textLinesToTranslate;
            }

            var textToTranslate = textLinesToTranslate.ToString();
            var translatedText = await translatorEngine.TranslateAsync(textToTranslate);
            var translatedTextEnumerator = translatedText.SplitToLines().GetEnumerator();

            foreach (var textLineToTranslate in textLinesToTranslate)
            {
                if (translatedTextEnumerator.MoveNext())
                {
                    textLineToTranslate.Text = translatedTextEnumerator.Current;
                }
            }

            return textLinesToTranslate;
        }
    }
}
