using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using VSTranslations.Adornments;
using VSTranslations.Glyphs;
using VSTranslations.Extensions;
using VSTranslations.Abstractions.Settings;
using VSTranslations.Abstractions.Adornments;
using VSTranslations.Abstractions.TextView;

namespace VSTranslations.Services.Adornments
{
    /// <summary>
    /// <see cref="IntraTextAdornmentTag"/> tager that provides intratext tags
    /// for view adornments.
    /// </summary>
    internal class TranslatedTextAdornmentTagger : ITagger<IntraTextAdornmentTag>
    {
        private readonly IWpfTextView _view;
        private readonly IAdornmentCache<TranslatedTextAdornment> _adornmentCache;
        private readonly ISnapshotSpansInvalidator _snapshotSpansInvalidator;
        private readonly ITagAggregator<TranslatedLineGlyphTag> _glyphTagger;
        private readonly IEditorSettings _editorSettings;
        private readonly PositionAffinity? _adornmentAffinity;
        private ITextSnapshot _textSnapshot;

        public TranslatedTextAdornmentTagger(IWpfTextView view,
            ITagAggregator<TranslatedLineGlyphTag> glyphTagger,
            IAdornmentCache<TranslatedTextAdornment> adornmentCache,
            ISnapshotSpansInvalidator snapshotSpansInvalidator,
            IEditorSettings editorSettings,
            PositionAffinity adornmentAffinity = PositionAffinity.Successor)
        {
            _view = view;
            _textSnapshot = view.TextBuffer.CurrentSnapshot;
            _glyphTagger = glyphTagger;
            _adornmentCache = adornmentCache;
            _snapshotSpansInvalidator = snapshotSpansInvalidator;
            _editorSettings = editorSettings;
            _adornmentAffinity = adornmentAffinity;

            _view.LayoutChanged += ViewLayoutChanged;
            _view.TextBuffer.Changed += TextContentChanged;
            _glyphTagger.TagsChanged += HandleDataTagsChanged;
            _snapshotSpansInvalidator.SpansInvalidated = () => _view.VisualElement.Dispatcher.BeginInvoke(new Action(AsyncUpdate));
        }

        /// <summary>
        /// An event handler to execute when there is a change in the tags.
        /// </summary>
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        /// <summary>
        /// Gets all tag spans with <see cref="IntraTextAdornmentTag"/> instances
        /// for the provided <paramref name="spans"/>.
        /// </summary>
        /// <param name="spans">Spans to get the tag spans for.</param>
        /// <returns>Tag spans.</returns>

        public IEnumerable<ITagSpan<IntraTextAdornmentTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            if (spans is null || spans.Count == 0)
            {
                yield break;
            }

            var requestedTextSnapshot = spans[0].Snapshot;
            var translatedSpans = new NormalizedSnapshotSpanCollection(spans.Select(span => span.TranslateTo(_textSnapshot, SpanTrackingMode.EdgeExclusive)));

            foreach (var tagSpan in GetAdornmentTagsOnSnapshot(translatedSpans))
            {
                var span = tagSpan.Span.TranslateTo(requestedTextSnapshot, SpanTrackingMode.EdgeExclusive);
                var tag = new IntraTextAdornmentTag(tagSpan.Tag.Adornment, tagSpan.Tag.RemovalCallback, tagSpan.Tag.Affinity);

                yield return new TagSpan<IntraTextAdornmentTag>(span, tag);
            }
        }

        private IEnumerable<TagSpan<IntraTextAdornmentTag>> GetAdornmentTagsOnSnapshot(NormalizedSnapshotSpanCollection spans)
        {
            var snapshot = spans[0].Snapshot;
            Debug.Assert(snapshot == _textSnapshot);

            var snapshotSpansToRemove = new HashSet<SnapshotSpan>(_adornmentCache.GetIntersectingSnapshotSpans(spans));

            foreach (var spanDataPair in _glyphTagger.GetTagSpansData(spans).Distinct(TranslatedLineGlyphTagComparer.Instance))
            {
                var (snapshotSpan, affinity, translatedTextTag) = spanDataPair;

                if (_adornmentCache.TryGet(snapshotSpan, out var adornment))
                {
                    adornment.Update(translatedTextTag);
                    snapshotSpansToRemove.Remove(snapshotSpan);
                }
                else
                {
                    adornment = new TranslatedTextAdornment(translatedTextTag);
                    adornment.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                    _adornmentCache.Set(snapshotSpan, adornment);
                }

                _editorSettings.ApplyCommentTextStyleTo(adornment);
                affinity ??= _adornmentAffinity;

                yield return new TagSpan<IntraTextAdornmentTag>(snapshotSpan, new IntraTextAdornmentTag(adornment, null, affinity));
            }

            _adornmentCache.Remove(snapshotSpansToRemove);
        }

        private void ViewLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            _adornmentCache.RemoveInactiveSpanEntries();
        }

        private void HandleDataTagsChanged(object sender, TagsChangedEventArgs args)
        {
            var changedSpans = args.Span.GetSpans(_view.TextBuffer.CurrentSnapshot);
            _snapshotSpansInvalidator.InvalidateSpans(changedSpans);
        }

        private void TextContentChanged(object sender, TextContentChangedEventArgs e)
        {
            var editedSpans = e.Changes.Select(change => new SnapshotSpan(e.After, change.NewSpan)).ToList();
            _snapshotSpansInvalidator.InvalidateSpans(editedSpans);
        }

        private void AsyncUpdate()
        {
            if (_textSnapshot != _view.TextBuffer.CurrentSnapshot)
            {
                _textSnapshot = _view.TextBuffer.CurrentSnapshot;
                _adornmentCache.Refresh(_textSnapshot);
            }

            var invalidatedSpans = _snapshotSpansInvalidator.ActivateInvalidatedSpans(_textSnapshot);
            if (invalidatedSpans.Count == 0)
            {
                return;
            }

            var start = invalidatedSpans.Select(span => span.Start).Min();
            var end = invalidatedSpans.Select(span => span.End).Max();

            TagsChanged?.Invoke(this, new SnapshotSpanEventArgs(new SnapshotSpan(start, end)));
        }
    }
}
