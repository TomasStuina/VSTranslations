using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace VSTranslations.Glyphs
{
    public class TranslatedLineGlyphTag : IGlyphTag
    {
        public TranslatedLineGlyphTag(SnapshotSpan span, string text)
        {
            Span = span.Snapshot.CreateTrackingSpan(span, SpanTrackingMode.EdgeExclusive);
            Text = text;
            IsActive = true;
        }

        public ITrackingSpan Span { get; }

        public string Text { get; }

        public bool IsActive { get; set; }
    }
}
