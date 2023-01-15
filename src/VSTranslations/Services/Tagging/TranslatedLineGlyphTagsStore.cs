using Microsoft.VisualStudio.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Glyphs;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Services.Tagging
{
    internal class TranslatedLineGlyphTagsStore : ITranslatedLineGlyphTagsStore
    {
        private readonly ObservableCollection<TranslatedLineGlyphTag> _translatedLineGlyphTags = new();

        public void Add(SnapshotSpan span, string text)
        {
            var translatedLineGlyphTag = new TranslatedLineGlyphTag(span, text.ThrowIfNull(nameof(text)));

            _translatedLineGlyphTags.Add(translatedLineGlyphTag);
        }

        public void Remove(TranslatedLineGlyphTag tag)
        {
            _translatedLineGlyphTags.Remove(tag.ThrowIfNull(nameof(tag)));
        }

        public void Subscribe(NotifyCollectionChangedEventHandler handler)
        {
            _translatedLineGlyphTags.CollectionChanged += handler.ThrowIfNull(nameof(handler));
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
