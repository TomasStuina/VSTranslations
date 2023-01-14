using Microsoft.VisualStudio.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Glyphs;

namespace VSTranslations.Services.Tagging
{
    internal class TranslatedLineGlyphTagsStore : ITranslatedLineGlyphTagsStore
    {
        private readonly ObservableCollection<TranslatedLineGlyphTag> _translatedLineGlyphTags = new();

        public void Add(SnapshotSpan span, string text)
        {
            if (text is null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var translatedLineGlyphTag = new TranslatedLineGlyphTag(span, text);

            _translatedLineGlyphTags.Add(translatedLineGlyphTag);
        }

        public void Remove(TranslatedLineGlyphTag tag)
        {
            if (tag is null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            _translatedLineGlyphTags.Remove(tag);
        }

        public void Subscribe(NotifyCollectionChangedEventHandler handler)
        {
            if (handler is not null)
            {
                _translatedLineGlyphTags.CollectionChanged += handler;
            }
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
