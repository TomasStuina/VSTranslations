using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Moq;
using System;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Adornments;
using VSTranslations.Services.Adornments;
using Xunit;
using VSTranslations.Delegates;
using System.Linq;
using Microsoft.VisualStudio.Text.Editor;

namespace VSTranslations.UnitTests.Services.Adornments
{
    public class InMemoryAdornmentCacheTests
    {
        [Theory]
        [InMemoryAdornmentCacheAutoData]
        public void Set_WhenAdornmentNull_ShouldThrowArgumentNullException(IFixture fixture,
            SnapshotSpan snapshotSpan)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();

            // Act & Assert
            FluentActions.Invoking(() => cache.Set(snapshotSpan, null))
                .Should().Throw<ArgumentNullException>();
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void Set_WhenAdornmentNotNull_ShouldThrowArgumentNullException(IFixture fixture,
            SnapshotSpan snapshotSpan, TranslatedTextAdornment adornment)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();

            // Act
            cache.Set(snapshotSpan, adornment);

            // Assert
            cache.Should().ContainSingle();
            cache.Should().ContainKey(snapshotSpan);
            cache.Should().ContainValue(adornment);
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void Set_WhenInvokedMoreThanOnceOnSameSnapshot_ShouldOverridePreviousAdornment(IFixture fixture,
            SnapshotSpan snapshotSpan, TranslatedTextAdornment previousAdornment, TranslatedTextAdornment adornment)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();

            // Act
            cache.Set(snapshotSpan, previousAdornment);
            cache.Set(snapshotSpan, adornment);

            // Assert
            cache.Should().ContainSingle();
            cache.Should().ContainKey(snapshotSpan);
            cache.Should().ContainValue(adornment);
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void TryGet_WhenAdornmentForSnapshotExists_ShouldReturnAsOutValue(IFixture fixture,
            SnapshotSpan snapshotSpan, TranslatedTextAdornment adornment)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();
            cache.Set(snapshotSpan, adornment);

            // Act
            var isSuccess = cache.TryGet(snapshotSpan, out var actualAdornment);

            // Assert
            isSuccess.Should().BeTrue();
            actualAdornment.Should().BeSameAs(adornment);
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void TryGet_WhenAdornmentForSnapshotDoesNotExist_ShouldNotReturnAsOutValue(IFixture fixture,
            SnapshotSpan snapshotSpan)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();

            // Act
            var isSuccess = cache.TryGet(snapshotSpan, out var actualAdornment);

            // Assert
            isSuccess.Should().BeFalse();
            actualAdornment.Should().BeNull();
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void Refresh_WhenTextViewNull_ShouldThrowArgumentNullException(IFixture fixture)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();

            // Act & Assert
            FluentActions.Invoking(() => cache.Refresh(null))
                .Should().Throw<ArgumentNullException>();
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void Refresh_WhenTextViewGiven_ShouldTranslateSpanToTextSnapshot(IFixture fixture,
            SnapshotSpan snapshotSpan, TranslatedTextAdornment adornment, ITextSnapshot textSnapshot,
            Mock<SpanTranslateDelegate> spanTranslate)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();
            cache.Set(snapshotSpan, adornment);

            // Act
            cache.Refresh(textSnapshot);

            // Assert
            spanTranslate.Verify(self => self(
                snapshotSpan, 
                textSnapshot),
                Times.Once);

            cache.Should().ContainSingle();
            cache.Should().ContainKey(snapshotSpan);
            cache.Should().ContainValue(adornment);
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void Refresh_WhenNoSpansInCache_ShouldNotTranslate(IFixture fixture,
            ITextSnapshot textSnapshot, Mock<SpanTranslateDelegate> spanTranslate)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();

            // Act
            cache.Refresh(textSnapshot);

            // Assert
            spanTranslate.Verify(self => self(
                It.IsAny<SnapshotSpan>(),
                textSnapshot),
                Times.Never);
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void GetIntersectingSnapshotSpans_WhenSnapshotSpansNull_ShouldThrowArgumentNullException(IFixture fixture)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();

            // Act & Assert
            FluentActions.Invoking(() => cache.GetIntersectingSnapshotSpans(null).ToList())
                .Should().Throw<ArgumentNullException>();
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void GetIntersectingSnapshotSpans_WhenGivenSpansInstersectWithCachedOnes_ShouldReturnThem(IFixture fixture,
            SnapshotSpan snapshotSpan, TranslatedTextAdornment adornment,
            NormalizedSnapshotSpanCollection snapshotSpans)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();
            cache.Set(snapshotSpan, adornment);

            // Act
            var spans = cache.GetIntersectingSnapshotSpans(snapshotSpans).ToList();

            // Assert
            spans.Should().ContainSingle()
                .Which.Should().Be(snapshotSpan);
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void GetIntersectingSnapshotSpans_WhenGivenSpansDoNotInstersectWithCachedOnes_ShouldNotReturnThem(IFixture fixture,
            SnapshotSpan snapshotSpan, TranslatedTextAdornment adornment, ITextSnapshot textSnapshot,
            NormalizedSnapshotSpanCollection snapshotSpans)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();
            cache.Set(new SnapshotSpan(textSnapshot, new Span(snapshotSpan.End + 1, 1)), adornment);

            // Act
            var spans = cache.GetIntersectingSnapshotSpans(snapshotSpans).ToList();

            // Assert
            spans.Should().BeEmpty();
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void Remove_WhenGivenSpansNull_ShouldThrowArgumentNullException(IFixture fixture)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();

            // Act & Assert
            FluentActions.Invoking(() => cache.Remove(null))
                .Should().Throw<ArgumentNullException>();
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void Remove_WhenGivenSpansExistInCache_ShouldRemoveThem(IFixture fixture,
            SnapshotSpan snapshotSpan, TranslatedTextAdornment adornment, ITextSnapshot textSnapshot)
        {
            // Arrange
            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();
            var otherSnapshotSpan = new SnapshotSpan(textSnapshot, new Span(snapshotSpan.End + 1, 1));
            cache.Set(snapshotSpan, adornment);
            cache.Set(otherSnapshotSpan, adornment);

            // Act
            cache.Remove(new[] { snapshotSpan });

            // Assert
            cache.Should().ContainSingle().Which.Key.Should().Be(otherSnapshotSpan);
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void RemoveInactiveSpanEntries_WhenCachedSpanDoesNotIntersectWithFormattedSpan_ShouldKeepOnlyIntersectingOnes(IFixture fixture, Mock<IWpfTextView> view,
            SnapshotSpan snapshotSpan, TranslatedTextAdornment adornment, ITextSnapshot textSnapshot)
        {
            // Arrange
            view.Setup(self => self.TextViewLines.FormattedSpan).Returns(snapshotSpan);

            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();
            var inactiveSpan = new SnapshotSpan(textSnapshot, new Span(snapshotSpan.End + 1, 1));
            cache.Set(snapshotSpan, adornment);
            cache.Set(inactiveSpan, adornment);

            // Act
            cache.RemoveInactiveSpanEntries();

            // Assert
            cache.Should().ContainSingle().Which.Key.Should().Be(snapshotSpan);
        }

        [StaTheory]
        [InMemoryAdornmentCacheAutoData]
        public void RemoveInactiveSpanEntries_WhenInvoked_ShouldTranslateAllCachedSpans(IFixture fixture, Mock<IWpfTextView> view,
            Mock<SpanTranslateDelegate> spanTranslate, SnapshotSpan snapshotSpan, TranslatedTextAdornment adornment, 
            ITextSnapshot textSnapshot)
        {
            // Arrange
            view.Setup(self => self.TextViewLines.FormattedSpan).Returns(snapshotSpan);

            var cache = fixture.Create<InMemoryAdornmentCache<TranslatedTextAdornment>>();
            var inactiveSpan = new SnapshotSpan(textSnapshot, new Span(snapshotSpan.End + 1, 1));
            cache.Set(snapshotSpan, adornment);
            cache.Set(inactiveSpan, adornment);

            // Act
            cache.RemoveInactiveSpanEntries();

            // Assert
            spanTranslate.Verify(self => self(snapshotSpan, textSnapshot), Times.Once);
            spanTranslate.Verify(self => self(inactiveSpan, textSnapshot), Times.Once);
        }
    }
}
