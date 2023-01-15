using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using VsTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Glyphs;
using VSTranslations.Services.Tagging;
using Xunit;

namespace VsTranslations.UnitTests.Services.Tagging
{
    public class TranslatedLineGlyphTaggerProviderTests
    {
        [Theory]
        [TranslatedLineGlyphTaggerProviderAutoData]
        public void CreateTagger_WhenInvokedWithTranslatedLineGlyphTagGeneric_ShouldReturnTranslatedLineGlyphTagTagger(IFixture fixture, ITextView textView, ITextBuffer textBuffer)
        {
            // Arrange
            var taggerProvider = fixture.Create<TranslatedLineGlyphTaggerProvider>();

            // Act
            var tagger = taggerProvider.CreateTagger<TranslatedLineGlyphTag>(textView, textBuffer);

            // Assert
            tagger.Should().BeOfType<TranslatedLineGlyphTagger>();
        }

        [Theory]
        [TranslatedLineGlyphTaggerProviderAutoData]
        public void CreateTagger_WhenInvokedWithTagInterfaceGeneric_ShouldReturnTranslatedLineGlyphTagTagger(IFixture fixture, ITextView textView, ITextBuffer textBuffer)
        {
            // Arrange
            var taggerProvider = fixture.Create<TranslatedLineGlyphTaggerProvider>();

            // Act
            var tagger = taggerProvider.CreateTagger<ITag>(textView, textBuffer);

            // Assert
            tagger.Should().BeOfType<TranslatedLineGlyphTagger>();
        }

        [Theory]
        [TranslatedLineGlyphTaggerProviderAutoData]
        public void CreateTagger_WhenInvokedWithAnyOtherTypeGeneric_ShouldReturnNull(IFixture fixture, ITextView textView, ITextBuffer textBuffer)
        {
            // Arrange
            var taggerProvider = fixture.Create<TranslatedLineGlyphTaggerProvider>();

            // Act
            var tagger = taggerProvider.CreateTagger<TestTag>(textView, textBuffer);

            // Assert
            tagger.Should().BeNull();
        }

        #region Helper methods

        private class TestTag : ITag
        {
        }

        #endregion
    }
}
