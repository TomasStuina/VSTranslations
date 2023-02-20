using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using VSTranslations.Extensions;
using VSTranslations.Glyphs;

namespace VSTranslations.Services.Tagging
{
    /// <summary>
    /// A concrete implementation of <see cref="IViewTaggerProvider"/>.
    /// </summary>
    [Export(typeof(IViewTaggerProvider))]
    [ContentType(ContentTypes.Text)]
    [TagType(typeof(TranslatedLineGlyphTag))]
    internal class TranslatedLineGlyphTaggerProvider : IViewTaggerProvider
    {
        /// <summary>
        /// Creates <see cref="TranslatedLineGlyphTagger"/>.
        /// </summary>
        /// <typeparam name="T">A type implementing <see cref="ITag"/>.</typeparam>
        /// <param name="textView">Text view to create for.</param>
        /// <param name="buffer">Buffer to use in tagger.</param>
        /// <returns><see cref="TranslatedLineGlyphTagger"/> instance.</returns>
        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            var translatedLineGlyphTagsStore = textView.GetOrCreateTranslatedLineGlyphTagsStore();

            return new TranslatedLineGlyphTagger(textView, buffer, translatedLineGlyphTagsStore) as ITagger<T>;
        }
    }
}
