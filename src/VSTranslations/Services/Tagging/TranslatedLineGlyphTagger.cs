using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using VSTranslations.Glyphs;
using VSTranslations.Abstractions.Tagging;
using System.Collections.Generic;
using System.Linq;

namespace VSTranslations.Services.Tagging
{
    internal class TranslatedLineGlyphTagger : ITagger<TranslatedLineGlyphTag>
    {
        private ITextView _view;
        private ITextBuffer _sourceBuffer;
        private ITranslatedLineGlyphTagsStore _translatedLineGlyphTagsStore;

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public TranslatedLineGlyphTagger(ITextView view, ITextBuffer sourceBuffer, ITranslatedLineGlyphTagsStore translatedLineGlyphTagsStore)
        {
            _view = view;
            _sourceBuffer = sourceBuffer;
            _translatedLineGlyphTagsStore = translatedLineGlyphTagsStore;
            _translatedLineGlyphTagsStore = translatedLineGlyphTagsStore;

            _view.Caret.PositionChanged += CaretPositionChanged;
            _view.LayoutChanged += ViewLayoutChanged;

            translatedLineGlyphTagsStore.Subscribe((_, _) =>
            {
                var snapshotSpan = new SnapshotSpan(_sourceBuffer.CurrentSnapshot, 0, _sourceBuffer.CurrentSnapshot.Length);
                TagsChanged?.Invoke(this, new SnapshotSpanEventArgs(snapshotSpan));
            });
        }

        private void ViewLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
        }

        private void CaretPositionChanged(object sender, CaretPositionChangedEventArgs e)
        {
        }

        public IEnumerable<ITagSpan<TranslatedLineGlyphTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var overlapingTagSpan in spans.SelectMany(GetOverlapingTags))
            {
                yield return overlapingTagSpan;
            }
        }

        private IEnumerable<ITagSpan<TranslatedLineGlyphTag>> GetOverlapingTags(SnapshotSpan snapshotSpan)
        {
            foreach (var translatedLineGlyphTag in _translatedLineGlyphTagsStore)
            {
                var glyphTagSpan = translatedLineGlyphTag.Span.GetSpan(snapshotSpan.Snapshot);
                if (glyphTagSpan.OverlapsWith(snapshotSpan))
                {
                    yield return new TagSpan<TranslatedLineGlyphTag>(glyphTagSpan, translatedLineGlyphTag);
                }
            }
        }
    }
}
