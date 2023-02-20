using Microsoft.VisualStudio.Text;
using System.Collections.Generic;

namespace VSTranslations.Abstractions.Adornments
{
    /// <summary>
    /// An adornment cache interface for caching adornments on the view.
    /// </summary>
    /// <typeparam name="TAdornment">The type of adornment.</typeparam>
    public interface IAdornmentCache<TAdornment> : IEnumerable<KeyValuePair<SnapshotSpan, TAdornment>>
    {
        /// <summary>
        /// Tries to get a cached adornment that matches the snapshot span.
        /// </summary>
        /// <param name="snapshotSpan">Snapshot span.</param>
        /// <param name="value">Output addornment instance.</param>
        /// <returns><see langword="true"/> if snapshot span exists. Otherwise - <see langword="false"/>.</returns>
        bool TryGet(SnapshotSpan snapshotSpan, out TAdornment value);

        /// <summary>
        /// Gets all the snapshot spans that instersect with
        /// the provided <paramref name="spans"/> collection.
        /// </summary>
        /// <param name="spans">Spans collection to intersect with.</param>
        /// <returns>An enumerable of intersecting spans.</returns>
        IEnumerable<SnapshotSpan> GetIntersectingSnapshotSpans(NormalizedSnapshotSpanCollection spans);

        /// <summary>
        /// Caches an adornment <paramref name="value"/>
        /// for the provided <paramref name="snapshotSpan"/>.
        /// </summary>
        /// <param name="snapshotSpan">Snapshot span to set for.</param>
        /// <param name="value">Addornment to cache.</param>
        void Set(SnapshotSpan snapshotSpan, TAdornment value);

        /// <summary>
        /// Removes all cached addorments that have
        /// the provided <paramref name="snapshotSpans"/> as
        /// keys.
        /// </summary>
        /// <param name="snapshotSpans">Snapshot spans to remove adornments for.</param>
        void Remove(IEnumerable<SnapshotSpan> snapshotSpans);

        /// <summary>
        /// Refreshes all snapshot spans to be cached
        /// for the provided <paramref name="textSnapshot"/>.
        /// </summary>
        /// <param name="textSnapshot">Test snapshot to use.</param>
        void Refresh(ITextSnapshot textSnapshot);

        /// <summary>
        /// Removes all the cached addorments with spans that
        /// are no longer preesent on the view.
        /// </summary>
        void RemoveInactiveSpanEntries();
    }
}
