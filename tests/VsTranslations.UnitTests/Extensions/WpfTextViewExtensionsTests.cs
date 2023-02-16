using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Moq;
using System;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Adornments;
using VSTranslations.Extensions;
using VSTranslations.Services.Adornments;
using VSTranslations.Services.TextView;
using Xunit;

namespace VSTranslations.UnitTests.Extensions
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

        [Fact]
        public void GetOrCreateAdornmentCache_WhenWpfTextViewIsNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            FluentActions.Invoking(() => WpfTextViewExtensions.GetOrCreateAdornmentCache<TranslatedTextAdornment>(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [WpfTextViewAutoData]
        public void GetOrCreateAdornmentCache_WhenWpfTextViewNotNull_ShouldReturnInMemoryAdornmentCache(IWpfTextView wpfTextView)
        {
            // Act & Assert
            wpfTextView.GetOrCreateAdornmentCache<TranslatedTextAdornment>()
                .Should().BeOfType<InMemoryAdornmentCache<TranslatedTextAdornment>>();
        }

        [Theory]
        [WpfTextViewAutoData]
        public void GetOrCreateAdornmentCache_WhenInvokedMoreThanOnce_ShouldReturnSameInstance(IWpfTextView wpfTextView)
        {
            // Act & Assert
            wpfTextView.GetOrCreateAdornmentCache<TranslatedTextAdornment>()
                .Should().BeSameAs(wpfTextView.GetOrCreateAdornmentCache<TranslatedTextAdornment>());
        }

        [Fact]
        public void GetOrCreateSnapshotSpansInvalidator_WhenWpfTextViewIsNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            FluentActions.Invoking(() => WpfTextViewExtensions.GetOrCreateSnapshotSpansInvalidator(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [WpfTextViewAutoData]
        public void GetOrCreateSnapshotSpansInvalidator_WhenWpfTextViewNotNull_ShouldReturnSnapshotSpansInvalidator(IWpfTextView wpfTextView)
        {
            // Act & Assert
            wpfTextView.GetOrCreateSnapshotSpansInvalidator()
                .Should().BeOfType<SnapshotSpansInvalidator>();
        }

        [Theory]
        [WpfTextViewAutoData]
        public void GetOrCreateSnapshotSpansInvalidator_WhenInvokedMoreThanOnce_ShouldReturnSameInstance(IWpfTextView wpfTextView)
        {
            // Act & Assert
            wpfTextView.GetOrCreateSnapshotSpansInvalidator()
                .Should().BeSameAs(wpfTextView.GetOrCreateSnapshotSpansInvalidator());
        }
    }
}
