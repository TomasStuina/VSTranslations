using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Moq;
using System.Linq;
using VsTranslations.UnitTests.AutoFixture.Attributes;
using VsTranslations.UnitTests.Common.Moq.Extensions;
using VSTranslations.Extensions;
using Xunit;
using MockFactory = VsTranslations.UnitTests.Moq.MockFactory;

namespace VsTranslations.UnitTests.Extensions
{
    public class SnapshotSpanExtensionsTests
    {
        [Theory]
        [SnapshotSpanAutoData]
        public void GetLinesCollection_WhenTextLineSpansRetrieved_ShouldReturnTrimmedAsTextLineCollection(IFixture fixture, SnapshotSpan snapshotSpan,
            Mock<ITextSnapshot> textSnapshot, string[] textLines)
        {
            // Arrange
            fixture.Register((ITextSnapshot currentTextSnapshot) =>
            {
                return new[]
                {
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 0, 30).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 30, 60).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 60, 90).Object
                };
            });

            textSnapshot
                .SetupSequence(self => self.GetText(It.IsAny<Span>()))
                .ReturnsMany(textLines.Select(textLine => $"   {textLine}   "));

            // Act
            var linesCollection = snapshotSpan.GetLinesCollection().ToList();

            // Assert
            linesCollection.Should().NotBeEmpty();
            linesCollection.Select(line => line.Text).Should().BeEquivalentTo(textLines);
        }

        [Theory]
        [SnapshotSpanInlineAutoData("{")]
        [SnapshotSpanInlineAutoData("}")]
        [SnapshotSpanInlineAutoData("};")]
        [SnapshotSpanInlineAutoData(",")]
        public void GetLinesCollection_WhenTextLineHasOpenningOrClosingChars_ShouldNotContainThatLine(string lineText, SnapshotSpan snapshotSpan,
            Mock<ITextSnapshot> textSnapshot, Mock<ITextSnapshotLine> textSnapshotLine)
        {
            // Arrange
            textSnapshotLine
                .SetupSequence(self => self.LineNumber)
                .ReturnsMany(1, 1);

            textSnapshot
                .Setup(self => self.GetText(It.IsAny<Span>()))
                .Returns(lineText);

            // Act
            var linesCollection = snapshotSpan.GetLinesCollection().ToList();

            // Assert
            linesCollection.Should().BeEmpty();
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void GetLinesSnapshotSpans_WhenStartAndEndOnSameLine_ShouldReturnSameSpan(SnapshotSpan snapshotSpan,
            Mock<ITextSnapshotLine> textSnapshotLine)
        {
            // Arrange
            textSnapshotLine
                .SetupSequence(self => self.LineNumber)
                .ReturnsMany(1, 1);

            // Act
            var snapshotSpans = snapshotSpan.GetLinesSnapshotSpans().ToList();

            // Assert
            var resultSnapshotSpan = snapshotSpans.Should().ContainSingle()
                .Which.Should().Match<SnapshotSpan>(span =>
                    span.Start == snapshotSpan.Start && span.End == snapshotSpan.End);
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void GetLinesSnapshotSpans_WhenStartAndEndOnDifferentLines_ShouldReturnLineSpans(IFixture fixture, SnapshotSpan snapshotSpan,
            Mock<ITextSnapshotLine> textSnapshotLine)
        {
            // Arrange
            fixture.Register((ITextSnapshot currentTextSnapshot) =>
            {
                return new[]
                {
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 0, 30).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 30, 60).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 60, 90).Object
                };
            });

            textSnapshotLine
                .SetupSequence(self => self.LineNumber)
                .ReturnsMany(1, 3);

            // Act
            var snapshotSpans = snapshotSpan.GetLinesSnapshotSpans().ToList();

            // Assert
            snapshotSpans.Should().HaveCount(3);
            snapshotSpans[0].Should().Match<SnapshotSpan>(span => span.Start == snapshotSpan.Start && span.End == snapshotSpans[0].End);
            snapshotSpans[1].Should().Match<SnapshotSpan>(span => span.Start == snapshotSpans[1].Start && span.End == snapshotSpans[1].End);
            snapshotSpans[2].Should().Match<SnapshotSpan>(span => span.Start == snapshotSpans[2].Start && span.End == snapshotSpan.End);
        }
    }
}
