using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System.Windows;
using VSTranslations.Glyphs;

namespace VSTranslations.Services.Tagging
{
    internal class TranslatedLineGlyphFactory : IGlyphFactory
    {
        public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
        {
            return new TranslatedLineGlyphTagView();
        }
    }
}
