using Microsoft.VisualStudio.Text.Editor;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Services.Tagging;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Extensions
{
    /// <summary>
    /// <see cref="ITextView"/> extensions.
    /// </summary>
    internal static class TextViewExtensions
    {
        /// <summary>
        /// Gets or creates <see cref="ITranslatedLineGlyphTagsStore"/> for the
        /// given <see cref="ITextView"/> instance.
        /// </summary>
        /// <param name="textView"><see cref="ITextView"/> instance to create for.</param>
        /// <returns><see cref="ITranslatedLineGlyphTagsStore"/> instance.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="textView"/> is <c>null</c>.</exception>
        public static ITranslatedLineGlyphTagsStore GetOrCreateTranslatedLineGlyphTagsStore(this ITextView textView)
        {
            return textView.ThrowIfNull(nameof(textView)).Properties.GetOrCreateSingletonProperty<ITranslatedLineGlyphTagsStore>(() => {
                return new TranslatedLineGlyphTagsStore();
            });
        }
    }
}
