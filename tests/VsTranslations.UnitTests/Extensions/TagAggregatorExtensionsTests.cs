using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Extensions;
using VSTranslations.Glyphs;
using Xunit;

namespace VSTranslations.UnitTests.Extensions
{
    public class TagAggregatorExtensionsTests
    {
        [Fact]
        public void GetTagSpansData_WhenTagAggregatorNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            FluentActions.Invoking(() => TagAggregatorExtensions.GetTagSpansData<TranslatedLineGlyphTag>(null, new NormalizedSnapshotSpanCollection()).ToList())
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTagSpansData_WhenSnapshoSpansNull_ShouldThrowArgumentNullException(ITagAggregator<TranslatedLineGlyphTag> tagAggregator)
        {
            // Act & Assert
            FluentActions.Invoking(() => tagAggregator.GetTagSpansData(null).ToList())
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTagSpansData_WhenSnapshoSpansEmpty_ShouldReturnEmpty(ITagAggregator<TranslatedLineGlyphTag> tagAggregator)
        {
            // Act & Assert
            tagAggregator.GetTagSpansData(new NormalizedSnapshotSpanCollection())
                .Should().BeEmpty();
        }

        [Theory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTagSpansData_WhenSnapshotSpanHasMappedTag_ShouldReturnTagData(ITagAggregator<TranslatedLineGlyphTag> tagAggregator,
            NormalizedSnapshotSpanCollection snapshotSpans, TranslatedLineGlyphTag tag, ITextSnapshotLine line)
        {
            // Act
            var tagsData = tagAggregator.GetTagSpansData(snapshotSpans).ToList();

            // Assert
            tagsData.Should().NotBeEmpty();
            tagsData.Should().AllSatisfy(item =>
            {
                var (itemSpan, itemAffinity, itemTag) = item;
                itemSpan.Start.Should().Be(line.End);
                itemSpan.End.Position.Should().Be(0);
                itemAffinity.Should().Be(PositionAffinity.Successor);
                itemTag.Should().Be(tag);
            });
        }

        [Theory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTagSpansData_WhenMappingSpanHasSpansAsNull_ShouldNotReturnData(IFixture fixture,
            Mock<IMappingTagSpan<TranslatedLineGlyphTag>> mappingTagSpan, NormalizedSnapshotSpanCollection snapshotSpans)
        {
            // Arrange
            mappingTagSpan
                .Setup(self => self.Span.GetSpans(It.IsNotNull<ITextSnapshot>()))
                .Returns<NormalizedSnapshotSpanCollection>(null);

            var tagAggregator = fixture.Create<ITagAggregator<TranslatedLineGlyphTag>>();

            // Act & Assert
            tagAggregator.GetTagSpansData(snapshotSpans)
                .Should().BeEmpty();
        }

        [Theory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTagSpansData_WhenMappingSpanHasSpansEmpty_ShouldNotReturnData(IFixture fixture,
            Mock<IMappingTagSpan<TranslatedLineGlyphTag>> mappingTagSpan, NormalizedSnapshotSpanCollection snapshotSpans)
        {
            // Arrange
            mappingTagSpan
                .Setup(self => self.Span.GetSpans(It.IsNotNull<ITextSnapshot>()))
                .Returns(new NormalizedSnapshotSpanCollection());

            var tagAggregator = fixture.Create<ITagAggregator<TranslatedLineGlyphTag>>();

            // Act & Assert
            tagAggregator.GetTagSpansData(snapshotSpans)
                .Should().BeEmpty();
        }

        [Theory]
        [TranslatedTextAdornmentTaggerAutoData]
        public void GetTagSpansData_WhenMappingSpanHasMoreThanOneSpan_ShouldNotReturnData(IFixture fixture,
            Mock<IMappingTagSpan<TranslatedLineGlyphTag>> mappingTagSpan, NormalizedSnapshotSpanCollection snapshotSpans, ITextSnapshot textSnapshot)
        {
            // Arrange
            mappingTagSpan
                .Setup(self => self.Span.GetSpans(It.IsNotNull<ITextSnapshot>()))
                .Returns(new NormalizedSnapshotSpanCollection(new List<SnapshotSpan>()
                {
                    new SnapshotSpan(textSnapshot, 0, 10),
                    new SnapshotSpan(textSnapshot, 20, 10)
                }));

            var tagAggregator = fixture.Create<ITagAggregator<TranslatedLineGlyphTag>>();

            // Act & Assert
            tagAggregator.GetTagSpansData(snapshotSpans)
                .Should().BeEmpty();
        }
    }
}
