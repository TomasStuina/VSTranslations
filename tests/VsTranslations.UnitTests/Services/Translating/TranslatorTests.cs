using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using VsTranslations.UnitTests.AutoFixture.Attributes;
using VsTranslations.UnitTests.Common.Moq.Extensions;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;
using Xunit;
using MockFactory = VsTranslations.UnitTests.Moq.MockFactory;

namespace VsTranslations.UnitTests.Services.Translating
{
    public class TranslatorTests
    {
        [Theory]
        [TranslatorAutoData]
        public async Task TranslateAsync_WhenTranslatorEngineNotPresent_ShouldReturnUntranslatedLines(IFixture fixture, SnapshotSpan snapshotSpan,
            Mock<ITranslatorEngineProvider> translatorEngineProvider, Mock<ITextSnapshot> textSnapshot,
            Mock<ITextSnapshotLine> textSnapshotLine, string[] textLines)
        {
            // Arrange
            translatorEngineProvider
                .Setup(self => self.Get())
                .Returns<ITranslatorEngine>(null);

            textSnapshot
                .SetupSequence(self => self.GetText(It.IsAny<Span>()))
                .ReturnsMany(textLines);

            fixture.Register((ITextSnapshot currentTextSnapshot) =>
            {
                return new[]
                {
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 0, 30).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 30, 60).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 60, 90).Object
                };
            });

            textSnapshotLine.SetupSequence(self => self.LineNumber).ReturnsMany(1, 3);

            var translator = fixture.Create<Translator>();

            // Act
            var translatedLinesCollection = await translator.TranslateAsync(snapshotSpan);

            // Assert
            translatedLinesCollection.Should().NotBeNull();

            translatedLinesCollection.Select(textLine => textLine.Text)
                .Should().BeEquivalentTo(textLines);
        }

        [Theory]
        [TranslatorAutoData]
        public async Task TranslateAsync_WhenSelectionSnapshotContainsLines_ShouldReturnTranslatedLinesCollection(IFixture fixture, SnapshotSpan snapshotSpan,
            Mock<ITranslatorEngine> translatorEngine, Mock<ITextSnapshot> textSnapshot,
            Mock<ITextSnapshotLine> textSnapshotLine, string[] textLines)
        {
            // Arrange
            var translatedTextLines = textLines.Select(textLine => "\u20AC" + textLine).ToArray();

            translatorEngine
                .Setup(self => self.TranslateAsync(It.IsAny<string>()))
                .Returns<string>(text => Task.FromResult(string.Join(Environment.NewLine, translatedTextLines)));

            textSnapshot
                .SetupSequence(self => self.GetText(It.IsAny<Span>()))
                .ReturnsMany(textLines);

            fixture.Register((ITextSnapshot currentTextSnapshot) =>
            {
                return new[]
                {
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 0, 30).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 30, 60).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 60, 90).Object
                };
            });

            textSnapshotLine.SetupSequence(self => self.LineNumber).ReturnsMany(1, 3);

            var translator = fixture.Create<Translator>();

            // Act
           var translatedLinesCollection = await translator.TranslateAsync(snapshotSpan);

            // Assert
            translatedLinesCollection.Should().NotBeNull();

            translatedLinesCollection.Select(textLine => textLine.Text)
                .Should().BeEquivalentTo(translatedTextLines);
        }

        [Theory]
        [TranslatorAutoData]
        public async Task TranslateAsync_WhenTranslatedCountIsLesser_ShouldKeepRemainingLinesUntranslated(IFixture fixture, SnapshotSpan snapshotSpan,
            Mock<ITranslatorEngine> translatorEngine, Mock<ITextSnapshot> textSnapshot,
            Mock<ITextSnapshotLine> textSnapshotLine, string[] textLines)
        {
            // Arrange
            var translatedTextLines = textLines.Take(1).Select(textLine =>  "\u20AC" + textLine).ToArray();

            translatorEngine
                .Setup(self => self.TranslateAsync(It.IsAny<string>()))
                .Returns<string>(text => Task.FromResult(string.Join(Environment.NewLine, translatedTextLines)));

            textSnapshot
                .SetupSequence(self => self.GetText(It.IsAny<Span>()))
                .ReturnsMany(textLines);

            fixture.Register((ITextSnapshot currentTextSnapshot) =>
            {
                return new[]
                {
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 0, 30).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 30, 60).Object,
                    MockFactory.MockTextSnaphotLine(currentTextSnapshot, 60, 90).Object
                };
            });

            textSnapshotLine.SetupSequence(self => self.LineNumber).ReturnsMany(1, 3);

            var translator = fixture.Create<Translator>();

            // Act
            var translatedLinesCollection = await translator.TranslateAsync(snapshotSpan);

            // Assert
            translatedLinesCollection.Should().NotBeNull();

            translatedLinesCollection.Select(textLine => textLine.Text)
                .Should().BeEquivalentTo(translatedTextLines.Concat(textLines.Skip(1)));
        }
    }
}