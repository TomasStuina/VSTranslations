using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using VSTranslations.Glyphs;

namespace VSTranslations.Abstractions.Tagging
{
    public interface ITranslatedLineGlyphTagsStore : IEnumerable<TranslatedLineGlyphTag>
    {
        void Add(SnapshotSpan span, string text);

        void Remove(TranslatedLineGlyphTag tag);
    }
}
