using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text;
using VSTranslations.Common.Extensions;
using VSTranslations.Abstractions.Adornments;
using VSTranslations.Services.Adornments;
using VSTranslations.Abstractions.TextView;
using VSTranslations.Services.TextView;

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
            textView.ThrowIfNull(nameof(textView));

            if (textView.Selection.IsEmpty)
            {
                var line = textView.Caret.Position.BufferPosition.GetContainingLine();

                return line.Extent;
            }

            return textView.Selection.SelectedSpans[0];
        }

        /// <summary>
        /// Gets or creates <see cref="IAdornmentCache{TAdornment}"/> service for
        /// the provided <paramref name="textView"/>.
        /// </summary>
        /// <typeparam name="TAdornment">Type of addornment.</typeparam>
        /// <param name="textView">WPF TextView to create for.</param>
        /// <returns><see cref="IAdornmentCache{TAdornment}"/> instance.</returns>
        public static IAdornmentCache<TAdornment> GetOrCreateAdornmentCache<TAdornment>(this IWpfTextView textView)
            where TAdornment : class
        {
            textView.ThrowIfNull(nameof(textView));

            return textView.Properties.GetOrCreateSingletonProperty<IAdornmentCache<TAdornment>>(() =>
            {
                return new InMemoryAdornmentCache<TAdornment>(textView);
            });
        }

        /// <summary>
        /// Gets or creates <see cref="ISnapshotSpansInvalidator"/> service for
        /// the provided <paramref name="textView"/>.
        /// </summary>
        /// <param name="textView">WPF TextView to create for.</param>
        /// <returns><see cref="ISnapshotSpansInvalidator"/> instance.</returns>
        public static ISnapshotSpansInvalidator GetOrCreateSnapshotSpansInvalidator(this IWpfTextView textView)
        {
            textView.ThrowIfNull(nameof(textView));

            return textView.Properties.GetOrCreateSingletonProperty<ISnapshotSpansInvalidator>(() =>
            {
                return new SnapshotSpansInvalidator();
            });
        }
    }
}
