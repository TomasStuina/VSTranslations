using Microsoft.VisualStudio.Text;
using System.Collections.Generic;

namespace VSTranslations.Abstractions.TextView
{
    /// <summary>
    /// An interface for snapshot spans invalidation.
    /// </summary>
    public interface ISnapshotSpansInvalidator
    {
        /// <summary>
        /// A delegate to execute after one or many
        /// snaphotspans are invalidated.
        /// </summary>
        Action SpansInvalidated { get; set; }

        /// <summary>
        /// Invalidates provided snaphot spans.
        /// </summary>
        /// <param name="spans">Snapshot spans to invaldiated.</param>
        void InvalidateSpans(IEnumerable<SnapshotSpan> spans);

        /// <summary>
        /// Activates invalidated spans for the provided <paramref name="textSnapshot"/>.
        /// </summary>
        /// <param name="textSnapshot">Text snapshot to activate for.</param>
        /// <returns>All invalidated spans.</returns>
        IList<SnapshotSpan> ActivateInvalidatedSpans(ITextSnapshot textSnapshot);
    }
}
