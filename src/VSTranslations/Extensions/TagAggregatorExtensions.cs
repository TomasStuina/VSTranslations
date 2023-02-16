using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using System.Collections.Generic;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Extensions
{
    /// <summary>
    /// <see cref="ITagAggregator{T}"/> extensions.
    /// </summary>
    internal static class TagAggregatorExtensions
    {
        /// <summary>
        /// Gets the pairs of snaphots spans, position affinity, and maching tags.
        /// </summary>
        /// <typeparam name="T">Type implementing <see cref="ITag"/>.</typeparam>
        /// <param name="tagAggregator">Tag aggragator to get the data from.</param>
        /// <param name="spans">Spans to get the data for.</param>
        /// <returns>An enumerable of snapshot spans and mathing tags.</returns>
        public static IEnumerable<Tuple<SnapshotSpan, PositionAffinity?, T>> GetTagSpansData<T>(this ITagAggregator<T> tagAggregator,
            NormalizedSnapshotSpanCollection spans)
            where T : ITag
        {
            tagAggregator.ThrowIfNull(nameof(tagAggregator));
            spans.ThrowIfNull(nameof(spans));

            if (spans.Count == 0)
            {
                yield break;
            }

            var snapshot = spans[0].Snapshot;

            foreach (var mappingTagSpan in tagAggregator.GetTags(spans))
            {
                var dataTagSpans = mappingTagSpan.Span.GetSpans(snapshot);
                if (dataTagSpans is null || dataTagSpans.Count != 1)
                {
                    continue;
                }

                var span = dataTagSpans[0];
                var line = span.Start.GetContainingLine();
                var adornmentSpan = new SnapshotSpan(line.End, 0);

                yield return Tuple.Create(adornmentSpan, (PositionAffinity?)PositionAffinity.Successor, mappingTagSpan.Tag);
            }
        }
    }
}
