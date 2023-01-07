using VSTranslations.Abstractions.Tagging;
using VSTranslations.Abstractions.Translating;

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
    }
}
