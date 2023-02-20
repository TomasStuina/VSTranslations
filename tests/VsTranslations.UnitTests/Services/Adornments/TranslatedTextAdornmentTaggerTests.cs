using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Moq;
using System.Collections.Generic;
using System.Linq;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Abstractions.Adornments;
using VSTranslations.Adornments;
using VSTranslations.Glyphs;
using VSTranslations.Services.Adornments;
using Xunit;

namespace VSTranslations.UnitTests.Services.Adornments
{
    public class TranslatedTextAdornmentTaggerTests
    {
        [StaTheory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTags_WhenSnapshotSpansNull_ShouldReturnEmpty(IFixture fixture)
        {
            // Arrange
            var addornmentTagger = fixture.Create<TranslatedTextAdornmentTagger>();

            // Act & Assert
            addornmentTagger.GetTags(null).Should().BeEmpty();
        }

        [StaTheory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTags_WhenSnapshotSpansEmpty_ShouldReturnEmpty(IFixture fixture)
        {
            // Arrange
            var addornmentTagger = fixture.Create<TranslatedTextAdornmentTagger>();

            // Act & Assert
            addornmentTagger.GetTags(new NormalizedSnapshotSpanCollection()).Should().BeEmpty();
        }

        [StaTheory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTags_WhenSnapshotSpanInstersectsWithCache_ShouldRemoveThromCache(IFixture fixture,
            NormalizedSnapshotSpanCollection snapshotSpans, ITextSnapshot textSnapshot,
            Mock<IAdornmentCache<TranslatedTextAdornment>> adornmentCache)
        {
            // Arrange
            var snapshotSpan = new SnapshotSpan(textSnapshot, new Span(0, 50));
            adornmentCache.Setup(self => self.GetIntersectingSnapshotSpans(snapshotSpans)).Returns(new[] { snapshotSpan });

            var addornmentTagger = fixture.Create<TranslatedTextAdornmentTagger>();

            // Act
            addornmentTagger.GetTags(snapshotSpans).ToList();

            // Assert
            adornmentCache.Verify(self => self.Remove(
                It.Is<IEnumerable<SnapshotSpan>>(collection => collection.Single() == snapshotSpan)),
                Times.Once);
        }

        [StaTheory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTags_WhenTagFromTaggerAndCached_ShouldUpdateAdornmentText(IFixture fixture, SnapshotSpan snapshotSpan,
            NormalizedSnapshotSpanCollection snapshotSpans, ITextSnapshotLine textSnapshotLine,
            Mock<IAdornmentCache<TranslatedTextAdornment>> adornmentCache,
            TranslatedLineGlyphTag glyphTag, string textToChange)
        {
            // Arrange
            var adornment = new TranslatedTextAdornment(new TranslatedLineGlyphTag(snapshotSpan, textToChange));
            adornmentCache
                .Setup(self => self.TryGet(
                    It.Is<SnapshotSpan>(snapshotSpan => snapshotSpan.Start.Position == textSnapshotLine.End && snapshotSpan.Length == 0),
                    out adornment))
                .Returns(true);

            var addornmentTagger = fixture.Create<TranslatedTextAdornmentTagger>();

            // Act
            var tagSpan = addornmentTagger.GetTags(snapshotSpans).ToList();

            // Assert
            adornment.TranslatedText.Text.Should().Be(glyphTag.Text);
        }

        [StaTheory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTags_WhenTagFromTaggerAndNotCached_ShouldAddSnapshotSpanToCache(IFixture fixture,
            NormalizedSnapshotSpanCollection snapshotSpans, ITextSnapshotLine textSnapshotLine,
            Mock<IAdornmentCache<TranslatedTextAdornment>> adornmentCache,
            TranslatedLineGlyphTag glyphTag)
        {
            // Arrange
            var addornmentTagger = fixture.Create<TranslatedTextAdornmentTagger>();

            // Act
            var tagSpan = addornmentTagger.GetTags(snapshotSpans).ToList();

            // Assert
            adornmentCache.Verify(self => self.Set(
                It.Is<SnapshotSpan>(snapshotSpan => snapshotSpan.Start.Position == textSnapshotLine.End && snapshotSpan.Length == 0),
                It.Is<TranslatedTextAdornment>(adornment => adornment.TranslatedText.Text == glyphTag.Text)),
                Times.Once);
        }
    }
}
