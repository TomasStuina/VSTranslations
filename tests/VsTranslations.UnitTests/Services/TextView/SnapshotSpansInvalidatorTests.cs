using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Services.TextView;
using Xunit;

namespace VSTranslations.UnitTests.Services.TextView
{
    public class SnapshotSpansInvalidatorTests
    {
        [Theory]
        [SnapshotSpanAutoData]
        public void InvalidateSpans_WhenSpansNull_ShouldThrowArgumentNullException(IFixture fixture)
        {
            // Arrange
            var invalidator = fixture.Create<SnapshotSpansInvalidator>();

            // Act & Assert
            FluentActions.Invoking(() => invalidator.InvalidateSpans(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void InvalidateSpans_WhenFirstSpansToInvalidate_ShouldInvokeSpansInvalidatedDelegate(IFixture fixture,
            IEnumerable<SnapshotSpan> snapshotSpans, Mock<Action> spansInvalidated)
        {
            // Arrange
            var invalidator = fixture.Create<SnapshotSpansInvalidator>();
            invalidator.SpansInvalidated = spansInvalidated.Object;

            // Act
            invalidator.InvalidateSpans(snapshotSpans);

            // Assert
            spansInvalidated.Verify(self => self(), Times.Once);
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void InvalidateSpans_WhenFirstSpansToInvalidateEmpty_ShouldNotInvokeSpansInvalidatedDelegate(IFixture fixture,
            Mock<Action> spansInvalidated)
        {
            // Arrange
            var invalidator = fixture.Create<SnapshotSpansInvalidator>();
            invalidator.SpansInvalidated = spansInvalidated.Object;

            // Act
            invalidator.InvalidateSpans(Enumerable.Empty<SnapshotSpan>());

            // Assert
            spansInvalidated.Verify(self => self(), Times.Never);
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void InvalidateSpans_WhenInvokedMoreThanOnce_ShouldInvokeSpansInvalidatedDelegateOnlyOnce(IFixture fixture,
            IEnumerable<SnapshotSpan> snapshotSpans, Mock<Action> spansInvalidated)
        {
            // Arrange
            var invalidator = fixture.Create<SnapshotSpansInvalidator>();
            invalidator.SpansInvalidated = spansInvalidated.Object;

            // Act
            invalidator.InvalidateSpans(snapshotSpans);
            invalidator.InvalidateSpans(snapshotSpans);

            // Assert
            spansInvalidated.Verify(self => self(), Times.Once);
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void ActivateInvalidatedSpans_WhenTextSnapshotNull_ShouldThrowArgumentNullException(IFixture fixture)
        {
            // Arrange
            var invalidator = fixture.Create<SnapshotSpansInvalidator>();

            // Act & Assert
            FluentActions.Invoking(() => invalidator.ActivateInvalidatedSpans(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void ActivateInvalidatedSpans_WhenInvoked_ShouldReturnInvalidatedSpans(IFixture fixture,
            IEnumerable<SnapshotSpan> snapshotSpans, ITextSnapshot textSnapshot)
        {
            // Arrange
            var invalidator = fixture.Create<SnapshotSpansInvalidator>();
            invalidator.InvalidateSpans(snapshotSpans);

            // Act
            var flushedSpans = invalidator.ActivateInvalidatedSpans(textSnapshot);

            // Assert
            flushedSpans.Should()
                .NotBeSameAs(snapshotSpans)
                .And.BeEquivalentTo(snapshotSpans);
        }

        [Theory]
        [SnapshotSpanAutoData]
        public void ActivateInvalidatedSpans_WhenInvoked_ShouldClearFromInvalidator(IFixture fixture,
            IEnumerable<SnapshotSpan> snapshotSpans, ITextSnapshot textSnapshot, Mock<Action> spansInvalidated)
        {
            // Arrange
            var invalidator = fixture.Create<SnapshotSpansInvalidator>();
            invalidator.InvalidateSpans(snapshotSpans);

            // Act
            invalidator.ActivateInvalidatedSpans(textSnapshot);
            invalidator.SpansInvalidated = spansInvalidated.Object;
            invalidator.InvalidateSpans(snapshotSpans);

            // Assert
            spansInvalidated.Verify(self => self(), Times.Once);
        }
    }
}
