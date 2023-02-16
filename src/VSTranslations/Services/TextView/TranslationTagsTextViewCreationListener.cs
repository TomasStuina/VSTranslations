using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using VSTranslations.Extensions;

namespace VSTranslations.Services.TextView
{
    /// <summary>
    /// A concrete implmentation of <see cref="ITextViewCreationListener"/>
    /// that is used to pre-initialize some of the services when a text view is created.
    /// </summary>
    [Export(typeof(ITextViewCreationListener))]
    [ContentType(ContentTypes.Text)]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal class TranslationTagsTextViewCreationListener : ITextViewCreationListener
    {
        /// <summary>
        /// Invoked when the <paramref name="textView"/> is created.
        /// Initializes <see cref="ITranslatedLineGlyphTagsStore"/> service.
        /// </summary>
        /// <param name="textView">A created text view.</param>
        public void TextViewCreated(ITextView textView)
        {
            textView.GetOrCreateTranslatedLineGlyphTagsStore();
        }
    }
}
