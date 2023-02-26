using Microsoft.VisualStudio.Threading;
using System.Runtime.InteropServices;
using System.Windows;
using VSTranslations.Abstractions.Translating;

namespace VSTranslations.Options
{
    [ComVisible(true)]
    public class GeneralOptionsPage : UIElementDialogPage
    {
        private readonly AsyncLazy<ITranslatorEngineProvider> _translatorEngineProvider;

        public GeneralOptionsPage()
        {
            _translatorEngineProvider = new AsyncLazy<ITranslatorEngineProvider>(() => VS.GetMefServiceAsync<ITranslatorEngineProvider>(),
                ThreadHelper.JoinableTaskFactory);
        }

        protected override UIElement Child =>
            new GeneralOptionsControl(new GeneralOptionsViewModel(_translatorEngineProvider.GetValue()));
    }
}
