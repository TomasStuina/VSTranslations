using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Services.Adornments;
using Xunit;

namespace VSTranslations.UnitTests.Services.Adornments
{
    public class TranslatedTextAdornmentTaggerProviderTests
    {
        [Theory]
        [TranslatedTextAdornmentTaggerProviderAutoData]
        public void CreateTagger_WhenTextViewNull_ShouldThrowArgumentNullException(IFixture fixture, IWpfTextView textView)
        {
            // Arrange
            var provider = fixture.Create<TranslatedTextAdornmentTaggerProvider>();

            // Act & Assert
            FluentActions.Invoking(() => provider.CreateTagger<IntraTextAdornmentTag>(null, textView.TextBuffer))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedTextAdornmentTaggerProviderAutoData]
        public void CreateTagger_WhenTextBufferNull_ShouldThrowArgumentNullException(IFixture fixture, IWpfTextView textView)
        {
            // Arrange
            var provider = fixture.Create<TranslatedTextAdornmentTaggerProvider>();

            // Act & Assert
            FluentActions.Invoking(() => provider.CreateTagger<IntraTextAdornmentTag>(textView, null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [TranslatedTextAdornmentTaggerProviderAutoData]
        public void CreateTagger_WhenTextViewBufferDoesNotMatchBuffer_ShouldReturnNull(IFixture fixture, ITextBuffer textBuffer, IWpfTextView textView)
        {
            // Arrange
            var provider = fixture.Create<TranslatedTextAdornmentTaggerProvider>();

            // Act
            var tagger = provider.CreateTagger<IntraTextAdornmentTag>(textView, textBuffer);

            // Assert
            tagger.Should().BeNull();
        }

        [Theory]
        [TranslatedTextAdornmentTaggerProviderAutoData]
        public void CreateTagger_WhenTextViewBufferMatchesBuffer_ShouldCreateTranslatedTextAdornmentTagger(IFixture fixture, IWpfTextView textView)
        {
            // Arrange
            var provider = fixture.Create<TranslatedTextAdornmentTaggerProvider>();

            // Act
            var tagger = provider.CreateTagger<IntraTextAdornmentTag>(textView, textView.TextBuffer);

            // Assert
            tagger.Should().BeOfType<TranslatedTextAdornmentTagger>();
        }

        [Theory]
        [TranslatedTextAdornmentTaggerProviderAutoData]
        public void CreateTagger_WhenInvokedMoreThanOnce_ShouldReturnSameTranslatedTextAdornmentTagger(IFixture fixture, IWpfTextView textView)
        {
            // Arrange
            var provider = fixture.Create<TranslatedTextAdornmentTaggerProvider>();

            // Act
            var tagger = provider.CreateTagger<IntraTextAdornmentTag>(textView, textView.TextBuffer);

            // Assert
            provider.CreateTagger<IntraTextAdornmentTag>(textView, textView.TextBuffer)
                .Should().BeSameAs(tagger);
        }
    }
}
