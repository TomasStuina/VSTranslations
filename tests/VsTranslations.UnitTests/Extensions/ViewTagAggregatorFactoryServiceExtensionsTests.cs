using FluentAssertions;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using VsTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Extensions;
using VSTranslations.Glyphs;
using Xunit;

namespace VsTranslations.UnitTests.Extensions
{
    public class ViewTagAggregatorFactoryServiceExtensionsTests
    {
        [Theory]
        [ViewTagAggregatorFactoryServiceAutoData]
        public void GetOrCreateTagAggregator_WhenViewTagAggregatorFactoryServiceNull_ShouldThrowArgumentNullException(ITextView textView)
        {
            // Act & Assert
            FluentActions.Invoking(() =>
                ViewTagAggregatorFactoryServiceExtensions.GetOrCreateTagAggregator<TranslatedLineGlyphTag>(null, textView))
                    .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [ViewTagAggregatorFactoryServiceAutoData]
        public void GetOrCreateTagAggregator_WhenInvokedFirstTime_ShouldCreateAndReturnTagAggregator(IViewTagAggregatorFactoryService viewTagAggregatorFactory,
            ITextView textView, ITagAggregator<TranslatedLineGlyphTag> tagAggregator)
        {
            // Act & Assert
            viewTagAggregatorFactory.GetOrCreateTagAggregator<TranslatedLineGlyphTag>(textView)
                .Should().BeSameAs(tagAggregator);
        }

        [Theory]
        [ViewTagAggregatorFactoryServiceAutoData]
        public void GetOrCreateTagAggregator_WhenInvokedMoreThanOnce_ShouldReturnSameTagAggregator(IViewTagAggregatorFactoryService viewTagAggregatorFactory,
            ITextView textView)
        {
            // Arrange
            var tagAggregator = viewTagAggregatorFactory.GetOrCreateTagAggregator<TranslatedLineGlyphTag>(textView);

            // Act & Assert
            viewTagAggregatorFactory.GetOrCreateTagAggregator<TranslatedLineGlyphTag>(textView)
                .Should().BeSameAs(tagAggregator);
        }
    }
}
