using System.Windows.Controls;
using VSTranslations.Glyphs;

namespace VSTranslations.Adornments
{
    /// <summary>
    /// Translated text addornment view.
    /// </summary>
    public partial class TranslatedTextAdornment : UserControl
    {
        public TranslatedTextAdornment(TranslatedLineGlyphTag tag)
        {
            InitializeComponent();
            Update(tag);
        }

        internal void Update(TranslatedLineGlyphTag tag)
        {
            TranslatedText.Text = tag.Text;
        }
    }
}
