using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Extensions;
using VSTranslations.Glyphs;

namespace VSTranslations.Services.Tagging
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType(Constants.TextContentType)]
    [ContentType(Constants.CodeContentType)]
    [TagType(typeof(TranslatedLineGlyphTag))]
    internal class TranslatedLineGlyphTaggerProvider : IViewTaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            var translatedLineGlyphTagsStore = textView.GetOrCreateTranslatedLineGlyphTagsStore();

            return new TranslatedLineGlyphTagger(textView, buffer, translatedLineGlyphTagsStore) as ITagger<T>;
        }
    }
}
