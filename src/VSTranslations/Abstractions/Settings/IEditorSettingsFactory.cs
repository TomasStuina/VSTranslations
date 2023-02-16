using Microsoft.VisualStudio.Text.Editor;

namespace VSTranslations.Abstractions.Settings
{
    /// <summary>
    /// An interface describing a factory for <see cref="IEditorSettings"/>.
    /// </summary>
    public interface IEditorSettingsFactory
    {
        /// <summary>
        /// Gets or creates a <see cref="IEditorSettings"/> instance
        /// for the provided <paramref name="textView"/>.
        /// </summary>
        /// <param name="textView">Text view instance to create for.</param>
        /// <returns><see cref="IEditorSettings"/> instance.</returns>
        IEditorSettings GetOrCreate(IWpfTextView textView);
    }
}
