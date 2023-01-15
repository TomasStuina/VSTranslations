using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Moq;
using System;
using System.Collections.Specialized;
using System.Linq;
using VsTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Glyphs;
using VSTranslations.Services.Tagging;
using Xunit;

namespace VsTranslations.UnitTests.Services.Tagging
{
    public class TranslatedLineGlyphTagsStoreTests
    {
        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Add_WhenTextIsNull_ShouldThrowArgumentNullException(IFixture fixture, SnapshotSpan snapshotSpan)
        {
            // Arrange
            var tagsStore = fixture.Create<TranslatedLineGlyphTagsStore>();

            // Act & Assert
            FluentActions.Invoking(() => tagsStore.Add(snapshotSpan, null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Add_WhenTextWithSpanPassed_ShouldAddAsTag(IFixture fixture, ITrackingSpan trackingSpan, SnapshotSpan snapshotSpan, string text)
        {
            // Arrange
            var tagsStore = fixture.Create<TranslatedLineGlyphTagsStore>();
        
            // Act
            tagsStore.Add(snapshotSpan, text);

            // Assert
            var tag = tagsStore.Should().ContainSingle().Subject;
            tag.Text.Should().Be(text);
            tag.Span.Should().BeSameAs(trackingSpan);
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Remove_WhenTagIsNull_ShouldThrowArgumentNullException(IFixture fixture)
        {
            // Arrange
            var tagsStore = fixture.Create<TranslatedLineGlyphTagsStore>();

            // Act & Assert
            FluentActions.Invoking(() => tagsStore.Remove(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Remove_WhenTagIsInStore_ShouldRemoveTag(IFixture fixture, SnapshotSpan snapshotSpan, string text)
        {
            // Arrange
            var tagsStore = fixture.Create<TranslatedLineGlyphTagsStore>();
            tagsStore.Add(snapshotSpan, text);

            // Act
            tagsStore.Remove(tagsStore.Single());

            // Assert
            tagsStore.Should().BeEmpty();
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Remove_WhenTagIsNotInStore_ShouldDoNothing(IFixture fixture, SnapshotSpan snapshotSpan, string text)
        {
            // Arrange
            var tagsStore = fixture.Create<TranslatedLineGlyphTagsStore>();
            tagsStore.Add(snapshotSpan, text);

            var tag = tagsStore.Single();

            // Act
            tagsStore.Remove(new TranslatedLineGlyphTag(snapshotSpan, text));

            // Assert
            tagsStore.Should().ContainSingle().Which.Should().BeSameAs(tag);
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Subscribe_WhenHandlerIsNull_ShouldThrowArgumentNullException(IFixture fixture)
        {
            // Arrange
            var tagsStore = fixture.Create<TranslatedLineGlyphTagsStore>();

            // Act & Assert
            FluentActions.Invoking(() => tagsStore.Subscribe(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Subscribe_WhenHandlerSubscribed_ShouldInvokeWhenAdding(IFixture fixture, SnapshotSpan snapshotSpan, string text)
        {
            // Arrange
            var tagsStore = fixture.Create<TranslatedLineGlyphTagsStore>();
            var handler = new Mock<NotifyCollectionChangedEventHandler>();

            // Act
            tagsStore.Subscribe(handler.Object);

            // Assert
            tagsStore.Add(snapshotSpan, text);
            handler.Verify(self => self(It.IsAny<object>(), It.IsNotNull<NotifyCollectionChangedEventArgs>()), Times.Once);
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Subscribe_WhenHandlerSubscribed_ShouldInvokeWhenRemoving(IFixture fixture, SnapshotSpan snapshotSpan, string text)
        {
            // Arrange
            var tagsStore = fixture.Create<TranslatedLineGlyphTagsStore>();
            tagsStore.Add(snapshotSpan, text);

            var handler = new Mock<NotifyCollectionChangedEventHandler>();

            // Act
            tagsStore.Subscribe(handler.Object);

            // Assert
            tagsStore.Remove(tagsStore.Single());
            handler.Verify(self => self(It.IsAny<object>(), It.IsNotNull<NotifyCollectionChangedEventArgs>()), Times.Once);
        }
    }
}
