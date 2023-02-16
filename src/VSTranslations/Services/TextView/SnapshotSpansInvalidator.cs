using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Linq;
using VSTranslations.Abstractions.TextView;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Services.TextView
{
    /// <summary>
    /// A concrete implmentation of <see cref="ISnapshotSpansInvalidator"/>.
    /// </summary>
    internal class SnapshotSpansInvalidator : ISnapshotSpansInvalidator
    {
        private readonly List<SnapshotSpan> _invalidatedSpans = new();

        /// <inheritdoc/>
        public Action SpansInvalidated { get; set; }

        /// <inheritdoc/>
        public void InvalidateSpans(IEnumerable<SnapshotSpan> spans)
        {
            spans.ThrowIfNull(nameof(spans));

            lock (_invalidatedSpans)
            {
                var wasEmpty = _invalidatedSpans.Count == 0;

                _invalidatedSpans.AddRange(spans);

                if (wasEmpty && _invalidatedSpans.Count > 0)
                {
                    SpansInvalidated?.Invoke();
                }
            }
        }

        /// <inheritdoc/>
        public IList<SnapshotSpan> ActivateInvalidatedSpans(ITextSnapshot textSnapshot)
        {
            textSnapshot.ThrowIfNull(nameof(textSnapshot));

            lock (_invalidatedSpans)
            {
                var translatedSpans = _invalidatedSpans.Select(s => s.TranslateTo(textSnapshot, SpanTrackingMode.EdgeInclusive)).ToList();
                _invalidatedSpans.Clear();

                return translatedSpans;
            }
        }
    }
}
