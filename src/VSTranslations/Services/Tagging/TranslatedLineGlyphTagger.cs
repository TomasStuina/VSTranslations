using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using VSTranslations.Glyphs;
using VSTranslations.Abstractions.Tagging;
using System.Collections.Generic;

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

            _view.Caret.PositionChanged += CaretPositionChanged;
            _view.LayoutChanged += ViewLayoutChanged;
            // _translatedTagsPersistence.TranslatedTextChanged += TranslatedTextChanged;
        }

        //private void TranslatedTextChanged(object sender, TranslatedTextChangedEventArgs e)
        //{
        //    TagsChanged?.Invoke(this, new SnapshotSpanEventArgs(new SnapshotSpan(SourceBuffer.CurrentSnapshot, 0, SourceBuffer.CurrentSnapshot.Length)));
        //}

        private void ViewLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
        }

        private void CaretPositionChanged(object sender, CaretPositionChangedEventArgs e)
        {
        }

        public IEnumerable<ITagSpan<TranslatedLineGlyphTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var span in spans)
            {
                var overlapingTagSpan = GetOverlapingTagSpan(span);
                if (overlapingTagSpan is not null)
                {
                    yield return overlapingTagSpan;
                }
            }
        }

        private ITagSpan<TranslatedLineGlyphTag> GetOverlapingTagSpan(SnapshotSpan snapshotSpan)
        {
            foreach (var translatedLineGlyphTag in _translatedLineGlyphTagsStore)
            {
                var glyphTagSpan = translatedLineGlyphTag.Span.GetSpan(snapshotSpan.Snapshot);
                if (glyphTagSpan.OverlapsWith(snapshotSpan))
                {
                    return new TagSpan<TranslatedLineGlyphTag>(glyphTagSpan, translatedLineGlyphTag);
                }
            }

            return null;
        }
    }
}
