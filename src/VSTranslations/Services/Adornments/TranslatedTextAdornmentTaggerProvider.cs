using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using VSTranslations.Abstractions.Settings;
using VSTranslations.Adornments;
using VSTranslations.Common.Extensions;
using VSTranslations.Extensions;
using VSTranslations.Glyphs;

namespace VSTranslations.Services.Adornments
{
    /// <summary>
    /// An implemention of <see cref="IViewTaggerProvider"/>
    /// for providing <see cref="TranslatedTextAdornmentTagger"/>.
    /// </summary>
    [Export(typeof(IViewTaggerProvider))]
    [ContentType(ContentTypes.Text)]
    [ContentType("projection")]
    [TagType(typeof(IntraTextAdornmentTag))]
    internal class TranslatedTextAdornmentTaggerProvider : IViewTaggerProvider
    {
        private readonly IViewTagAggregatorFactoryService _viewTagAggregatorFactory;
        private readonly IEditorSettingsFactory _editorSettingsFactory;

        [ImportingConstructor]
        public TranslatedTextAdornmentTaggerProvider(IViewTagAggregatorFactoryService viewTagAggregatorFactory, IEditorSettingsFactory editorSettingsFactory)
        {
            _viewTagAggregatorFactory = viewTagAggregatorFactory;
            _editorSettingsFactory = editorSettingsFactory;
        }

        /// <summary>
        /// Gets a <see cref="TranslatedTextAdornmentTagger"/> instance for
        /// the provided <paramref name="textView"/>.
        /// </summary>
        /// <typeparam name="T">A type implementing <see cref="ITag"/>.</typeparam>
        /// <param name="textView">Text view to get for.</param>
        /// <param name="buffer"><see cref="ITextBuffer"/> for a sanity check.</param>
        /// <returns>
        /// <see cref="TranslatedTextAdornmentTagger"/> instance.
        /// <see langword="null"/> if the text buffer in the <paramref name="textView"/>
        /// does not match <paramref name="buffer"/>.
        /// </returns>
        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            textView.ThrowIfNull(nameof(textView));
            buffer.ThrowIfNull(nameof(buffer));

            if (buffer != textView.TextBuffer)
            {
                return null;
            }

            return textView.Properties.GetOrCreateSingletonProperty(() =>
            {
                var wpfTextView = (IWpfTextView) textView;

                return new TranslatedTextAdornmentTagger(
                    wpfTextView,
                    _viewTagAggregatorFactory.GetOrCreateTagAggregator<TranslatedLineGlyphTag>(textView),
                    wpfTextView.GetOrCreateAdornmentCache<TranslatedTextAdornment>(),
                    wpfTextView.GetOrCreateSnapshotSpansInvalidator(),
                    _editorSettingsFactory.GetOrCreate(wpfTextView));
            }) as ITagger<T>;
        }
    }
}
