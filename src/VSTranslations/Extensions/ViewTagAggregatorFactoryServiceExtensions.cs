using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;

namespace VSTranslations.Extensions
{
    /// <summary>
    /// <see cref="IViewTagAggregatorFactoryService"/> extensions.
    /// </summary>
    internal static class ViewTagAggregatorFactoryServiceExtensions
    {
        /// <summary>
        /// Gets or creates <see cref="ITagAggregator{T}"/> for the current <see cref="ITextView"/> instance.
        /// </summary>
        /// <typeparam name="T">Tag type.</typeparam>
        /// <param name="viewTagAggregatorFactory"><see cref="IViewTagAggregatorFactoryService"/> to use for instantiating.</param>
        /// <param name="textView"><see cref="ITextView"/> instance to get/create for.</param>
        /// <returns><see cref="ITagAggregator{T}"/> instance.</returns>
        /// <exception cref="ArgumentNullException">
        /// When <paramref name="viewTagAggregatorFactory"/> or <paramref name="textView"/> is <c>null</c>.
        /// </exception>
        public static ITagAggregator<T> GetOrCreateTagAggregator<T>(this IViewTagAggregatorFactoryService viewTagAggregatorFactory, ITextView textView)
            where T : ITag
        {
            if (viewTagAggregatorFactory is null)
            {
                throw new ArgumentNullException(nameof(viewTagAggregatorFactory));
            }

            if (textView is null)
            {
                throw new ArgumentNullException(nameof(textView));
            }

            return textView.Properties.GetOrCreateSingletonProperty(() =>
            {
                return viewTagAggregatorFactory.CreateTagAggregator<T>(textView);
            });
        }
    }
}
