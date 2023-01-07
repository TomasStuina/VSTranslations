using Microsoft.VisualStudio.Text;
using System.Collections;
using System.Collections.Generic;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Glyphs;

namespace VSTranslations.Services.Tagging
{
    internal class TranslatedLineGlyphTagsStore : ITranslatedLineGlyphTagsStore
    {
        private List<TranslatedLineGlyphTag> _translatedLineGlyphTags = new List<TranslatedLineGlyphTag>();


        public void Add(SnapshotSpan span, string text)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var translatedLineGlyphTag = new TranslatedLineGlyphTag(span, text);

            _translatedLineGlyphTags.Add(translatedLineGlyphTag);

            //TranslatedTextChanged?.Invoke(this, new TranslatedTextChangedEventArgs(translatedTextTag, null));
        }

        public void Remove(TranslatedLineGlyphTag tag)
        {
            if (tag is null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            _translatedLineGlyphTags.Remove(tag);

            //TranslatedTextChanged?.Invoke(this, new TranslatedTextChangedEventArgs(null, translatedTextTag));
        }

        public IEnumerator<TranslatedLineGlyphTag> GetEnumerator()
        {
            return _translatedLineGlyphTags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _translatedLineGlyphTags.GetEnumerator();
        }
    }
}
