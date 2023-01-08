using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text;

namespace VSTranslations.Extensions
{
    /// <summary>
    /// <see cref="IWpfTextView"/> extensions.
    /// </summary>
    internal static class WpfTextViewExtensions
    {
        /// <summary>
        /// Gets a <see cref="SnapshotSpan"/> value from the
        /// provided <see cref="IWpfTextView"/> instance.
        /// </summary>
        /// <param name="textView"><see cref="IWpfTextView"/> instance to get from.</param>
        /// <remarks>
        /// If the selection is not empty then returns the first <see cref="SnapshotSpan"/>
        /// from the selected spans. Otherwise, returns <see cref="SnapshotSpan"/> of the
        /// line where the mouse cursor position is.
        /// </remarks>
        /// <returns><see cref="SnapshotSpan"/> instance.</returns>
        public static SnapshotSpan GetSelectedSnapshotSpan(this IWpfTextView textView)
        {
            if (textView.Selection.IsEmpty)
            {
                var line = textView.Caret.Position.BufferPosition.GetContainingLine();

                return line.Extent;
            }

            return textView.Selection.SelectedSpans[0];
        }
    }
}
