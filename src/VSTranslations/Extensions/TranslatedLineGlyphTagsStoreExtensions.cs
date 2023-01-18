using System.Collections.Generic;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Common.Extensions;
using VSTranslations.Glyphs;

namespace VSTranslations.Extensions
{
    internal static class TranslatedLineGlyphTagsStoreExtensions
    {
        public static void Add(this ITranslatedLineGlyphTagsStore translatedLineGlyphTagsStore, TextLinesCollection textLines)
        {
            translatedLineGlyphTagsStore.ThrowIfNull(nameof(translatedLineGlyphTagsStore));
            textLines.ThrowIfNull(nameof(textLines));

            foreach (var textLine in textLines)
            {
                translatedLineGlyphTagsStore.Add(textLine);
            }
        }

        public static void Add(this ITranslatedLineGlyphTagsStore translatedLineGlyphTagsStore, TextLine textLine)
        {
            translatedLineGlyphTagsStore.ThrowIfNull(nameof(translatedLineGlyphTagsStore));
            textLine.ThrowIfNull(nameof(textLine));

            translatedLineGlyphTagsStore.Add(textLine.LineSpan, textLine.Text);
        }

        public static void RemoveTags(this ITranslatedLineGlyphTagsStore translatedLineGlyphTagsStore, IEnumerable<TranslatedLineGlyphTag> glyphTags)
        {
            translatedLineGlyphTagsStore.ThrowIfNull(nameof(translatedLineGlyphTagsStore));
            glyphTags.ThrowIfNull(nameof(glyphTags));

            foreach (var glyphTag in glyphTags)
            {
                translatedLineGlyphTagsStore.Remove(glyphTag);
            }
        }
    }
}
