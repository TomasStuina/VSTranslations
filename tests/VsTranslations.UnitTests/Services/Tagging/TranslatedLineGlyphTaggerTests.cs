using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Glyphs;
using VSTranslations.Services.Tagging;
using Xunit;

namespace VSTranslations.UnitTests.Services.Tagging
{
    public class TranslatedLineGlyphTaggerTests
    {
        [Theory]
        [TranslatedLineGlyphTaggerAutoData]
        public void Ctor_WhenInitialized_ShouldRegisterLayoutAndPositionChangedHandlers(IFixture fixture, Mock<ITextView> textView)
        {
            // Act
            var tagger = fixture.Create<TranslatedLineGlyphTagger>();

            // Assert
            textView.VerifyAdd(self => self.Caret.PositionChanged += It.IsAny<EventHandler<CaretPositionChangedEventArgs>>(), Times.Once);
            textView.VerifyAdd(self => self.LayoutChanged += It.IsAny<EventHandler<TextViewLayoutChangedEventArgs>>(), Times.Once);
        }

        [Theory]
        [TranslatedLineGlyphTaggerAutoData]
        public void Ctor_WhenInitialized_ShouldSubscribeToTagsStore(IFixture fixture, Mock<ITranslatedLineGlyphTagsStore> store)
        {
            // Act
            var tagger = fixture.Create<TranslatedLineGlyphTagger>();

            // Assert
            store.Verify(self => self.Subscribe(It.IsNotNull<NotifyCollectionChangedEventHandler>()), Times.Once);
        }

        [Theory]
        [TranslatedLineGlyphTaggerAutoData]
        public void GetTags_WhenTagsOverlapWithTagsInStore_ShouldReturnOverlapingTags(IFixture fixture, NormalizedSnapshotSpanCollection snapshotSpans,
            IEnumerable<TranslatedLineGlyphTag> translatedLineGlyphTags)
        {
            // Arrange
            var tagger = fixture.Create<TranslatedLineGlyphTagger>();

            // Act
            var tags = tagger.GetTags(snapshotSpans).ToList();

            // Assert
            tags.Should().NotBeNull();
            tags.Select(tag => tag.Tag).Should().BeEquivalentTo(translatedLineGlyphTags);
        }

        [Theory]
        [TranslatedLineGlyphTaggerAutoData]
        public void GetTags_WhenTagsDoNotOverlapWithTagsInStore_ShouldNotReturnThoseTags(IFixture fixture, NormalizedSnapshotSpanCollection snapshotSpans,
            Mock<ITrackingSpan> trackingSpan, SnapshotSpan snapshotSpan)
        {
            // Arrange
            trackingSpan.Setup(self => self.GetSpan(It.IsAny<ITextSnapshot>())).Returns(new SnapshotSpan(snapshotSpan.End, 1));

            var tagger = fixture.Create<TranslatedLineGlyphTagger>();

            // Act
            var tags = tagger.GetTags(snapshotSpans).ToList();

            // Assert
            tags.Should().NotBeNull();
            tags.Select(tag => tag.Tag).Should().BeEmpty();
        }
    }
}
