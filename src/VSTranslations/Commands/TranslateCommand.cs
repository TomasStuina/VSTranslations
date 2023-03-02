using Microsoft.VisualStudio.Text.Editor;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Extensions;

namespace VSTranslations
{
    [Command(PackageIds.VSTranslations_ContextTranslateMenu_TranslateId)]
    internal sealed class TranslateCommand : BaseCommand<TranslateCommand>
    {
        private ITranslator _translator;

        protected async override Task InitializeCompletedAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            _translator = await VS.GetMefServiceAsync<ITranslator>();
        }

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var documentView = await VS.Documents.GetActiveDocumentViewAsync();
            if (documentView.TextView is null)
            {
                return;
            }
            await ExecuteAsync(documentView.TextView);
        }

        private async Task ExecuteAsync(IWpfTextView view)
        {
            try
            {
                await VS.StatusBar.StartAnimationAsync(StatusAnimation.Sync);
                await VS.StatusBar.ShowMessageAsync("Translating...");

                var span = view.GetSelectedSnapshotSpan();
                var translatedLines = await _translator.TranslateAsync(span);

                var glyphTagsStore = view.GetOrCreateTranslatedLineGlyphTagsStore();
                glyphTagsStore.Add(translatedLines);

                await VS.StatusBar.ShowMessageAsync("Translated");
            }
            catch(Exception ex)
            {
                await VS.StatusBar.ClearAsync();
                await VS.Windows.WriteToOutputAsync(Vsix.Name, ex.Message);
                await view.ShowInfoBarErrorAsync("Error occurred while trying to translate.");
            }
            finally
            {
                await VS.StatusBar.EndAnimationAsync(StatusAnimation.Sync);
            }
        }
    }
}
