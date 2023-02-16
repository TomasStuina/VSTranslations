using Microsoft.VisualStudio.Text;
using System.Collections.Generic;

namespace VSTranslations.Glyphs
{
    /// <summary>
    /// An equality comparer implementation for
    /// comparing <see cref="TranslatedLineGlyphTag"/> instances,
    /// based in their spans.
    /// </summary>
    internal class TranslatedLineGlyphTagComparer : IEqualityComparer<Tuple<SnapshotSpan, PositionAffinity?, TranslatedLineGlyphTag>>
    {
        private TranslatedLineGlyphTagComparer()
        {
        }

        /// <summary>
        /// A singleton instance of <see cref="TranslatedLineGlyphTagComparer"/>.
        /// </summary>
        public static TranslatedLineGlyphTagComparer Instance { get; } = new TranslatedLineGlyphTagComparer();

        /// <summary>
        /// Determines whether two snapshot span data entries are the same.
        /// </summary>
        /// <param name="x">Snapshot span data entry.</param>
        /// <param name="y">Snapshot span data entry to compare against.</param>
        /// <returns><see langword="true"/> if both have matching spans. Otherwise - <see langword="false"/></returns>
        public bool Equals(Tuple<SnapshotSpan, PositionAffinity?, TranslatedLineGlyphTag> x, Tuple<SnapshotSpan, PositionAffinity?, TranslatedLineGlyphTag> y)
        {
            if (x is null && y is null)
            {
                return true;
            }

            if (x is null || y is null)
            {
                return false;
            }

            return x.Item1.Equals(y.Item1);
        }

        /// <summary>
        /// Gets a hashcode for a snapshot span data entry.
        /// </summary>
        /// <param name="entry">Snapshot span data entry.</param>
        /// <returns>Hashcode value. <c>0</c> if <paramref name="entry"/> is <see langword="null"/>.</returns>
        public int GetHashCode(Tuple<SnapshotSpan, PositionAffinity?, TranslatedLineGlyphTag> entry)
        {
            return entry is not null ? entry.Item1.GetHashCode() : 0;
        }
    }
}
