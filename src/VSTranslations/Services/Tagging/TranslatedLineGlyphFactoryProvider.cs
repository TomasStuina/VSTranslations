using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows;
using VSTranslations.Glyphs;

namespace VSTranslations.Services.Tagging
{
    /// <summary>
    /// A concrete implemention of <see cref="IGlyphFactoryProvider"/>
    /// that can provide <see cref="TranslatedLineGlyphFactory"/>.
    /// </summary>
    [Export(typeof(IGlyphFactoryProvider))]
    [Name(nameof(TranslatedLineGlyphTag))]
    [Order(Before = Constants.VsTextMarker)]
    [ContentType(ContentTypes.Text)]
    [TagType(typeof(TranslatedLineGlyphTag))]
    internal sealed class TranslatedLineGlyphFactoryProvider : IGlyphFactoryProvider
    {
        /// <summary>
        /// This method creates an instance of our custom glyph factory for a given text view.
        /// </summary>
        /// <param name="view">The text view we are creating a glyph factory for, we don't use this.</param>
        /// <param name="margin">The glyph margin for the text view, we don't use this.</param>
        /// <returns>An instance of our custom glyph factory.</returns>
        public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin)
        {
            return new TranslatedLineGlyphFactory();
        }

        private class TranslatedLineGlyphFactory : IGlyphFactory
        {
            public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
            {
                return new TranslatedLineGlyphTagView();
            }
        }
    }
}
