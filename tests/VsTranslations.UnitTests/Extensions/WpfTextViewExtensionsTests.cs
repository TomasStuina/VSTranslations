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
using VSTranslations.UnitTests.Xunit;
using Microsoft.VisualStudio.Sdk.TestFramework;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSTranslations.UnitTests.Extensions
{
    public class WpfTextViewExtensionsTests : VsTestBase
    {
        public WpfTextViewExtensionsTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

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

        [Theory]
        [WpfTextViewAutoData]
        public async Task ShowInfoBarErrorAsync_WhenWpfTextViewIsNull_ShouldThrowArgumentNullException(string message)
        {
            await FluentActions.Invoking(() => WpfTextViewExtensions.ShowInfoBarErrorAsync(null, message))
                .Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [WpfTextViewAutoData]
        public async Task ShowInfoBarErrorAsync_WhenWpfTextViewIsNotNull_ShouldTryToCreateInfoBarWithMessage(IWpfTextView wpfTextView,
            IVsUIShell uiShell, Mock<IVsInfoBarUIFactory> infoBarUIFactory, string message)
        {
            ServiceProvider.AddService(typeof(SVsInfoBarUIFactory), infoBarUIFactory.Object);
            ServiceProvider.AddService(typeof(SVsUIShell), uiShell);

            await wpfTextView.ShowInfoBarErrorAsync(message);

            infoBarUIFactory
                .Verify(self => self.CreateInfoBar(
                    It.Is<IVsInfoBar>(infoBar => infoBar.TextSpans.GetSpan(0).Text == message)),
                        Times.Once);
        }
    }
}
