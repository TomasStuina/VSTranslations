using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using VSTranslations.Abstractions.Translating;

namespace VSTranslations.Extensions
{
    public static class SnapshotSpanExtensions
    {
        public static TextLinesCollection GetLines(this SnapshotSpan span)
        {
            var textLinesCollection = new TextLinesCollection();

            foreach (var lineSpan in span.GetLinesAsSnapshots())
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

        public static IEnumerable<string> GetLinesTrimmedText(this SnapshotSpan span)
        {
            foreach (var lineSpan in span.GetLinesAsSnapshots())
            {
                yield return lineSpan.GetText().Trim();
            }
        }

        public static IEnumerable<SnapshotSpan> GetLinesAsSnapshots(this SnapshotSpan span)
        {
            var startLineNumber = span.Start.GetContainingLine().LineNumber;
            var endLineNumber = (span.End - 1).GetContainingLine().LineNumber;

            if (startLineNumber == endLineNumber)
            {
                yield return span;
                yield break;
            }

            var snapshot = span.Snapshot;
            for (int i = startLineNumber; i <= endLineNumber; i++)
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
