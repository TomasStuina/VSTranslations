using Microsoft.VisualStudio.Text;

namespace VSTranslations.Delegates
{
    /// <summary>
    /// A delegate for describing snapshot span translation.
    /// </summary>
    /// <param name="snapshotSpan">Snapshot span to translate.</param>
    /// <param name="textSnapshot">Text snapshot to translate for.</param>
    /// <returns>Translated snapshot span.</returns>
    public delegate SnapshotSpan SpanTranslateDelegate(SnapshotSpan snapshotSpan, ITextSnapshot textSnapshot);
}
