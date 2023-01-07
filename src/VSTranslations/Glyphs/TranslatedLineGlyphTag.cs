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
        }

        public ITrackingSpan Span { get; }

        public string Text { get; }
    }
}
