using System.Collections.Generic;
using System.Linq;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Glyphs;

namespace VSTranslations.Extensions
{
    internal static class TranslatedLineGlyphTagsStoreExtensions
    {
        public static void Add(this ITranslatedLineGlyphTagsStore translatedTagsPersistence, TextLinesCollection textLines)
        {
            foreach (var textLine in textLines)
            {
                translatedTagsPersistence.Add(textLine);
            }
        }

        public static void Add(this ITranslatedLineGlyphTagsStore translatedTagsPersistence, TextLine textLine)
        {
            translatedTagsPersistence.Add(textLine.LineSpan, textLine.Text);
        }

        public static void RemoveTags(this ITranslatedLineGlyphTagsStore translatedTagsPersistence, IEnumerable<TranslatedLineGlyphTag> glyphTags)
        {
            foreach(var glyphTag in glyphTags)
            {
                translatedTagsPersistence.Remove(glyphTag);
            }
        }
    }
}
