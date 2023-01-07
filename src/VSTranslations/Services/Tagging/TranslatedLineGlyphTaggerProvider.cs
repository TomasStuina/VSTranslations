using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Glyphs;

namespace VSTranslations.Services.Tagging
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType(Constants.TextContentType)]
    [TagType(typeof(TranslatedLineGlyphTag))]
    internal class TranslatedLineGlyphTaggerProvider : IViewTaggerProvider
    {
        [Import]
        internal ITranslatedLineGlyphTagsStore TranslatedLineGlyphTagsStore { get; set; }

        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            return new TranslatedLineGlyphTagger(textView, buffer, TranslatedLineGlyphTagsStore) as ITagger<T>;
        }
    }
}
