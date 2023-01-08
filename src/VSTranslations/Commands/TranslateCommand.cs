using Microsoft.VisualStudio.Text.Editor;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Extensions;
using VSTranslations.Services.Translating;

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
            var span = view.GetSelectedSnapshotSpan();
            var translatedLines = await _translator.TranslateAsync(span);

            var glyphTagsStore = view.GetOrCreateTranslatedLineGlyphTagsStore();
            glyphTagsStore.Add(translatedLines);
        }
    }
}
