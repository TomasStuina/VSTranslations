using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using System.Linq;
using VSTranslations.Extensions;
using VSTranslations.Glyphs;

namespace VSTranslations.Commands
{
    [Command(PackageIds.VSTranslations_ContextTranslateMenu_DeleteTranslationId)]
    internal sealed class DeleteTranslationCommand : BaseCommand<DeleteTranslationCommand>
    {

        private IViewTagAggregatorFactoryService _viewTagAggregatorFactory;

        protected async override Task InitializeCompletedAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            _viewTagAggregatorFactory = await VS.GetMefServiceAsync<IViewTagAggregatorFactoryService>();
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

        private Task ExecuteAsync(IWpfTextView view)
        {
            var snapshotSpan = view.GetSelectedSnapshotSpan();
            var glyphTagsStore = view.GetOrCreateTranslatedLineGlyphTagsStore();
            var tagAggregator = _viewTagAggregatorFactory.GetOrCreateTagAggregator<TranslatedLineGlyphTag>(view);

            glyphTagsStore.RemoveTags(tagAggregator.GetTags(snapshotSpan).Select(mappingTag => mappingTag.Tag).ToList());

            return Task.CompletedTask;
        }
    }
}
