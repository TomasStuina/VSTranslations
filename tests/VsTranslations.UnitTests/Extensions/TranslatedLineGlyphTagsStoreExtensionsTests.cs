using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Moq;
using System;
using System.Collections.Generic;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Extensions;
using VSTranslations.Glyphs;
using Xunit;

namespace VSTranslations.UnitTests.Extensions
{
    public class TranslatedLineGlyphTagsStoreExtensionsTests
    {
        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]

        public void Add_WhenStoreNullWithTextLines_ShouldThrowArgumentNullException(TextLinesCollection textLines)
        {
            // Act & Assert
            FluentActions.Invoking(() => TranslatedLineGlyphTagsStoreExtensions.Add(null, textLines))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Add_WhenStoreNullWithTextLine_ShouldThrowArgumentNullException(TextLine textLine)
        {
            // Act & Assert
            FluentActions.Invoking(() => TranslatedLineGlyphTagsStoreExtensions.Add(null, textLine))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Add_WhenTextLinesNull_ShouldThrowArgumentNullException(ITranslatedLineGlyphTagsStore translatedLineGlyphTags)
        {
            // Act & Assert
            FluentActions.Invoking(() => translatedLineGlyphTags.Add((TextLinesCollection)null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Add_WhenTextLineNull_ShouldThrowArgumentNullException(ITranslatedLineGlyphTagsStore translatedLineGlyphTags)
        {
            // Act & Assert
            FluentActions.Invoking(() => translatedLineGlyphTags.Add((TextLine)null))
                .Should().Throw<ArgumentNullException>();
        }
        
        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Add_WhenTextLinesNotEmpty_ShouldAddAsTags(ITranslatedLineGlyphTagsStore translatedLineGlyphTags,
            TextLinesCollection textLines)
        {
            // Act
            translatedLineGlyphTags.Add(textLines);

            // Assert
            foreach(var textLine in textLines)
            {
                Mock.Get(translatedLineGlyphTags).Verify(self => self.Add(It.IsAny<SnapshotSpan>(), textLine.Text), Times.Once);
            }
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void Add_WhenTextLineGiven_ShouldAddAsTag(ITranslatedLineGlyphTagsStore translatedLineGlyphTags,
            TextLine textLine)
        {
            // Act
            translatedLineGlyphTags.Add(textLine);

            // Assert
            Mock.Get(translatedLineGlyphTags).Verify(self => self.Add(It.IsAny<SnapshotSpan>(), textLine.Text), Times.Once);
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]

        public void RemoveTags_WhenStoreNull_ShouldThrowArgumentNullException(IEnumerable<TranslatedLineGlyphTag> lineGlyphTags)
        {
            // Act & Assert
            FluentActions.Invoking(() => TranslatedLineGlyphTagsStoreExtensions.RemoveTags(null, lineGlyphTags))
                .Should().Throw<ArgumentNullException>();
        }


        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]

        public void RemoveTags_WhenGlyphTagsNull_ShouldThrowArgumentNullException(ITranslatedLineGlyphTagsStore translatedLineGlyphTags)
        {
            // Act & Assert
            FluentActions.Invoking(() => translatedLineGlyphTags.RemoveTags(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedLineGlyphTagsStoreAutoData]
        public void RemoveTags(ITranslatedLineGlyphTagsStore translatedLineGlyphTags, IEnumerable<TranslatedLineGlyphTag> lineGlyphTags)
        {
            // Act
            translatedLineGlyphTags.RemoveTags(lineGlyphTags);

            // Assert
            foreach (var glyphTag in lineGlyphTags)
            {
                Mock.Get(translatedLineGlyphTags).Verify(self => self.Remove(glyphTag), Times.Once);
            }
        }
    }
}
