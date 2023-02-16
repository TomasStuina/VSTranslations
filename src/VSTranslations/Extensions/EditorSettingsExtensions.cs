using VSTranslations.Abstractions.Settings;
using VSTranslations.Adornments;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Extensions
{
    /// <summary>
    /// <see cref="IEditorSettings"/> extensions.
    /// </summary>
    internal static class EditorSettingsExtensions
    {
        /// <summary>
        /// Applies font settings from the <paramref name="editorSettings"/>
        /// to the provided <paramref name="adornment"/>.
        /// </summary>
        /// <param name="editorSettings">Editor settings.</param>
        /// <param name="adornment">Addornment to apply for.</param>
        public static void ApplyCommentTextStyleTo(this IEditorSettings editorSettings, TranslatedTextAdornment adornment)
        {
            editorSettings.ThrowIfNull(nameof(editorSettings));
            adornment.ThrowIfNull(nameof(adornment));

            adornment.TranslatedTextBox.FontSize = editorSettings.FontSize;
            adornment.TranslatedTextBox.FontFamily = editorSettings.FontFamily;
            adornment.TranslatedTextBox.Foreground = editorSettings.CommentForegroundColor;
        }
    }
}
