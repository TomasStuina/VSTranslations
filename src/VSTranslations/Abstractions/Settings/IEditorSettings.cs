using System.Windows.Media;

namespace VSTranslations.Abstractions.Settings
{
    /// <summary>
    /// An interface for providing editor settings,
    /// like font information, color, etc.
    /// </summary>
    public interface IEditorSettings
    {
        /// <summary>
        /// Returns <see cref="Brush"/> instance
        /// that matches the color of the comments foreground.
        /// </summary>
        Brush CommentForegroundColor { get; }

        /// <summary>
        /// Returns <see cref="Brush"/> instance
        /// that matches the color of the comments background.
        /// </summary>
        Brush CommentBackgroundColor { get; }

        /// <summary>
        /// Returns font family used in the editor.
        /// </summary>
        FontFamily FontFamily { get; }

        /// <summary>
        /// Returns font size used in the editor.
        /// </summary>
        double FontSize { get; }
    }
}
