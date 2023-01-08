using FluentAssertions;
using Microsoft.VisualStudio.Text.Editor;
using System;
using VsTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Extensions;
using VSTranslations.Services.Tagging;
using Xunit;
using TextViewExtensions = VSTranslations.Extensions.TextViewExtensions;

namespace VsTranslations.UnitTests.Extensions
{
    public class TextViewExtensionsTests
    {
        [Fact]
        public void GetOrCreateTranslatedLineGlyphTagsStore_WhenTextViewNull_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            FluentActions.Invoking(() => TextViewExtensions.GetOrCreateTranslatedLineGlyphTagsStore(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TextViewAutoData]
        public void GetOrCreateTranslatedLineGlyphTagsStore_WhenInvokedFirstTime_ShouldCreateAndReturnStore(ITextView textView)
        {
            // Act
            var glyphTagsStore = textView.GetOrCreateTranslatedLineGlyphTagsStore();

            // Assert
            glyphTagsStore.Should().NotBeNull();
            glyphTagsStore.Should().BeOfType<TranslatedLineGlyphTagsStore>();
        }

        [Theory]
        [TextViewAutoData]
        public void GetOrCreateTranslatedLineGlyphTagsStore_WhenInvokedMoreThanOnce_ShouldReturnSameInstance(ITextView textView)
        {
            // Arrange
            var glyphTagsStore = textView.GetOrCreateTranslatedLineGlyphTagsStore();

            // Act & Assert
            textView.GetOrCreateTranslatedLineGlyphTagsStore()
                .Should().BeSameAs(glyphTagsStore);
        }
    }
}
