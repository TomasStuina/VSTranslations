using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using VSTranslations.Extensions;

namespace VSTranslations.Services.TextView
{
    [Export(typeof(ITextViewCreationListener))]
    [ContentType(Constants.TextContentType)]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal class TranslationTagsTextViewCreationListener : ITextViewCreationListener
    {
        public void TextViewCreated(ITextView textView)
        {
            textView.GetOrCreateTranslatedLineGlyphTagsStore();
        }
    }
}
