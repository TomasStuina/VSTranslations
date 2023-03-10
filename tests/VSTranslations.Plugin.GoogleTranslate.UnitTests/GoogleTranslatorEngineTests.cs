using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Sdk.TestFramework;
using Moq;
using System.Net;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Plugin.GoogleTranslate.UnitTests.AutoFixture.Attributes;
using VSTranslations.Plugin.GoogleTranslate.UnitTests.Xunit;
using VSTranslations.UnitTests.Common;
using Xunit;

namespace VSTranslations.Plugin.GoogleTranslate.UnitTests
{
    public class GoogleTranslatorEngineTests : VsTestBase
    {
        public GoogleTranslatorEngineTests(GlobalServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [Theory]
        [GoogleTranslatorEngineInlineAutoData(null)]
        [GoogleTranslatorEngineInlineAutoData("")]
        [GoogleTranslatorEngineInlineAutoData(" ")]
        public async Task TranslateAsync_WhenTextNullOrWhiteSpace_ShouldReturnEmptyText(string text, IFixture fixture)
        {
            // Arrange
            var engine = fixture.Create<GoogleTranslatorEngine>();

            // Act
            var translatedText = await engine.TranslateAsync(text);

            // Assert
            translatedText.Should().BeEmpty();
        }

        [Theory]
        [GoogleTranslatorEngineAutoData]
        public async Task TranslateAsync_WhenTextProvided_ShouldSendHttpRequest(IFixture fixture, Language source, Language target,
            HttpResponseMessage httpResponse, Mock<HttpMessageDelegate> messageDelegate, string text)
        {
            // Arrange
            httpResponse.StatusCode = HttpStatusCode.BadRequest;
            var engineConfig = fixture.Create<Mock<ITranslatorEngineConfig<GoogleTranslatorEngine>>>();
            engineConfig.Setup(self => self.GetSourceLanguageAsync()).ReturnsAsync(source);
            engineConfig.Setup(self => self.GetTargetLanguageAsync()).ReturnsAsync(target);

            var engine = fixture.Create<GoogleTranslatorEngine>();

            // Act
            await engine.TranslateAsync(text);

            // Assert
            messageDelegate.Verify(self => self(
                It.Is<HttpRequestMessage>(message =>
                    message.Method == HttpMethod.Get
                        && message.RequestUri.ToString() == $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={source.Code}"
                            + $"&tl={target.Code}&hl=en-US&dt=t&dt=bd&dj=1&source=icon&tk=310461.310461&q={text}")
                ), Times.Once);
        }

        [Theory]
        [GoogleTranslatorEngineAutoData]
        public async Task TranslateAsync_WhenResponseSuccessfulWithTranslatedLines_ShouldReturnTranslatedText(IFixture fixture,
            HttpResponseMessage httpResponse, string text, IEnumerable<string> lines)
        {
            // Arrange
            httpResponse.Content = CreateGoogleTranslateResponseContent(lines);
            var engine = fixture.Create<GoogleTranslatorEngine>();

            // Act
            var translatedText = await engine.TranslateAsync(text);

            // Assert
            translatedText.Should().Be(string.Join(string.Empty, lines));
        }

        [Theory]
        [GoogleTranslatorEngineAutoData]
        public async Task TranslateAsync_WhenResponseSuccessfulWithLinesNull_ShouldReturnSameText(IFixture fixture,
            HttpResponseMessage httpResponse, string text)
        {
            // Arrange
            httpResponse.Content = new StringContent("{\"sentences\":null}");
            var engine = fixture.Create<GoogleTranslatorEngine>();

            // Act
            var translatedText = await engine.TranslateAsync(text);

            // Assert
            translatedText.Should().Be(text);
        }

        #region Helper methods

        private static StringContent CreateGoogleTranslateResponseContent(IEnumerable<string> lines)
        {
            var linesJoined = string.Join(",", lines.Select(line => $"{{\"trans\":\"{line}\"}}"));

            return new StringContent($"{{\"sentences\":[{linesJoined}]}}");
        }


        #endregion
    }
}