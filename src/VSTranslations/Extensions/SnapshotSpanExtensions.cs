using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Extensions
{
    /// <summary>
    /// <see cref="SnapshotSpan"/> extensions.
    /// </summary>
    internal static class SnapshotSpanExtensions
    {
        /// <summary>
        /// Gets a <see cref="TextLinesCollection"/> instance for the given <see cref="SnapshotSpan"/>.
        /// </summary>
        /// <remarks>
        /// Empty or only '{', '}', ';', or ',' character containg lines are omitted.
        /// </remarks>
        /// <param name="span"><see cref="SnapshotSpan"/> to get for.</param>
        /// <returns><see cref="TextLinesCollection"/> instance</returns>
        public static TextLinesCollection GetLinesCollection(this SnapshotSpan span)
        {
            var textLinesCollection = new TextLinesCollection();

            foreach (var lineSpan in span.GetLinesSnapshotSpans())
            {
                var trimmedLine = lineSpan.GetText().Trim();
                if (trimmedLine.Length == 0 || trimmedLine.IsOpening() || trimmedLine.IsClosing())
                {
                    continue;
                }

                textLinesCollection.Add(new TextLine(lineSpan, trimmedLine));
            }

            return textLinesCollection;
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{SnapshotSpan}"/> of lines from the given
        /// <see cref="SnapshotSpan"/> instance.
        /// </summary>
        /// <param name="span"><see cref="SnapshotSpan"/> instance to get from.</param>
        /// <returns><see cref="IEnumerable{SnapshotSpan}"/> instance.</returns>
        public static IEnumerable<SnapshotSpan> GetLinesSnapshotSpans(this SnapshotSpan span)
        {
            var startLineNumber = span.Start.GetContainingLine().LineNumber;
            var endLineNumber = (span.End - 1).GetContainingLine().LineNumber;

            if (startLineNumber == endLineNumber)
            {
                yield return span;
                yield break;
            }

            var snapshot = span.Snapshot;
            for (var i = startLineNumber; i <= endLineNumber; i++)
            {
                var line = snapshot.GetLineFromLineNumber(i);
                if (i == startLineNumber)
                {
                    yield return new SnapshotSpan(span.Start, line.End);
                    continue;
                }

                if (i == endLineNumber)
                {
                    yield return new SnapshotSpan(line.Start, span.End);
                    continue;
                }

                yield return new SnapshotSpan(line.Start, line.End);
            }
        }
    }
}
