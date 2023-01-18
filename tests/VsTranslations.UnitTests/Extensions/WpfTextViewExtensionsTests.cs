using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Moq;
using System;
using VsTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Extensions;
using Xunit;

namespace VsTranslations.UnitTests.Extensions
{
    public class WpfTextViewExtensionsTests
    {
        [Fact]
        public void GetSelectedSnapshotSpan_WhenWpfTextViewIsNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            FluentActions.Invoking(() => WpfTextViewExtensions.GetSelectedSnapshotSpan(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [WpfTextViewAutoData]
        public void GetSelectedSnapshotSpan_WhenSelectionEmpty_ShouldReturnExtentSpanOfLineInCarretPosition(Mock<IWpfTextView> textView,
            Mock<ITextSnapshotLine> textSnaphotLine, SnapshotSpan snapshotSpan)
        {
            // Arrange
            textView.Setup(self => self.Selection.IsEmpty).Returns(true);
            textSnaphotLine.Setup(self => self.Extent).Returns(snapshotSpan);

            // Act & Assert
            textView.Object.GetSelectedSnapshotSpan()
                .Should().Match<SnapshotSpan>(selectedSpan => selectedSpan.Start == snapshotSpan.Start
                    && selectedSpan.End == snapshotSpan.End);
        }

        [Theory]
        [WpfTextViewAutoData]
        public void GetSelectedSnapshotSpan_WhenSelectionIsNotEmpty_ShouldReturnSelectedSpan(Mock<IWpfTextView> textView, SnapshotSpan snapshotSpan)
        {
            // Arrange
            textView.Setup(self => self.Selection.IsEmpty).Returns(false);

            // Act & Assert
            textView.Object.GetSelectedSnapshotSpan()
                .Should().Match<SnapshotSpan>(selectedSpan => selectedSpan.Start == snapshotSpan.Start
                    && selectedSpan.End == snapshotSpan.End);
        }
    }
}
