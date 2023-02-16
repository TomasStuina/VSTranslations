using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VSTranslations.Abstractions.Adornments;
using VSTranslations.Common.Extensions;
using VSTranslations.Delegates;

namespace VSTranslations.Services.Adornments
{
    /// <summary>
    /// An in memory implementation of <see cref="IAdornmentCache{TAdornment}"/>.
    /// </summary>
    /// <typeparam name="TAdornment">A reference type.</typeparam>
    internal class InMemoryAdornmentCache<TAdornment> : IAdornmentCache<TAdornment> where TAdornment : class
    {
        private Dictionary<SnapshotSpan, TAdornment> _adornmentCache = new();
        private readonly IWpfTextView _view;
        private readonly SpanTranslateDelegate _spanTranslate;

        public InMemoryAdornmentCache(IWpfTextView view)
            : this(view.ThrowIfNull(nameof(view)), TranslateSnapshot)
        {
            
        }

        internal InMemoryAdornmentCache(IWpfTextView view, SpanTranslateDelegate spanTranslate)
        {
            _view = view;
            _spanTranslate = spanTranslate;
        }

        /// <inheritdoc/>
        public void Set(SnapshotSpan snapshotSpan, TAdornment value)
        {
            _adornmentCache[snapshotSpan] = value.ThrowIfNull(nameof(value));
        }

        /// <inheritdoc/>
        public bool TryGet(SnapshotSpan snapshotSpan, out TAdornment value)
        {
            return _adornmentCache.TryGetValue(snapshotSpan, out value);
        }

        /// <inheritdoc/>
        public void Refresh(ITextSnapshot textSnapshot)
        {
            textSnapshot.ThrowIfNull(nameof(textSnapshot));

            var translatedAdornmentCache = new Dictionary<SnapshotSpan, TAdornment>();

            foreach (var keyValuePair in _adornmentCache)
            {
                translatedAdornmentCache.Add(_spanTranslate(keyValuePair.Key, textSnapshot), keyValuePair.Value);
            }

            _adornmentCache = translatedAdornmentCache;
        }

        /// <inheritdoc/>
        public IEnumerable<SnapshotSpan> GetIntersectingSnapshotSpans(NormalizedSnapshotSpanCollection spans)
        {
            spans.ThrowIfNull(nameof(spans));

            foreach (var addornment in _adornmentCache)
            {
                if (spans.IntersectsWith(new NormalizedSnapshotSpanCollection(addornment.Key)))
                {
                    yield return addornment.Key;
                }
            }
        }

        /// <inheritdoc/>
        public void Remove(IEnumerable<SnapshotSpan> snapshotSpans)
        {
            _adornmentCache.RemoveAll(snapshotSpans.ThrowIfNull(nameof(snapshotSpans)));
        }

        /// <inheritdoc/>
        public void RemoveInactiveSpanEntries()
        {
            var visibleSpan = _view.TextViewLines.FormattedSpan;
            var inactiveSnapshotSpans = _adornmentCache
                .Where(entry => !_spanTranslate(entry.Key, visibleSpan.Snapshot).IntersectsWith(visibleSpan))
                .Select(entry => entry.Key)
                .ToList();

            _adornmentCache.RemoveAll(inactiveSnapshotSpans);
        }

        public IEnumerator<KeyValuePair<SnapshotSpan, TAdornment>> GetEnumerator() => _adornmentCache.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _adornmentCache.GetEnumerator();

        private static SnapshotSpan TranslateSnapshot(SnapshotSpan span, ITextSnapshot textSnapshot) =>
            span.TranslateTo(textSnapshot, SpanTrackingMode.EdgeExclusive);
    }
}
