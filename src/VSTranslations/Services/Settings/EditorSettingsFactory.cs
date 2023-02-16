using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Threading;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using VSTranslations.Abstractions.Settings;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Services.Settings
{
    /// <summary>
    /// An implementation of a factory for creating <see cref="EditorSettings"/>.
    /// </summary>
    [Export(typeof(IEditorSettingsFactory))]
    internal class EditorSettingsFactory : IEditorSettingsFactory
    {
        private readonly AsyncLazy<IVsFontAndColorStorage> _fontAndColorStorage;

        public EditorSettingsFactory() : this(VS.Services.GetFontAndColorStorageAsync)
        {
        }

        internal EditorSettingsFactory(Func<Task<IVsFontAndColorStorage>> fontColorStorageFactory)
        {
            _fontAndColorStorage = new AsyncLazy<IVsFontAndColorStorage>(fontColorStorageFactory,
                ThreadHelper.JoinableTaskFactory);
        }

        /// <summary>
        /// Gets or create <see cref="EditorSettings"/> instance for the provided
        /// <paramref name="textView"/>.
        /// </summary>
        /// <param name="textView">Text view to create for.</param>
        /// <returns><see cref="EditorSettings"/> instance.</returns>
        public IEditorSettings GetOrCreate(IWpfTextView textView)
        {
            textView.ThrowIfNull(nameof(textView));

            return textView.Properties.GetOrCreateSingletonProperty<IEditorSettings>(() =>
            {
                return new EditorSettings(_fontAndColorStorage.GetValue());
            });
        }
    }
}
