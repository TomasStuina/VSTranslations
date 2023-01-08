using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;

namespace VSTranslations.Extensions
{
    internal static class ViewTagAggregatorFactoryServiceExtensions
    {
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
