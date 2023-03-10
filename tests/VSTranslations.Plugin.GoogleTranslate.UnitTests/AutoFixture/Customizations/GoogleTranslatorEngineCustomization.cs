using AutoFixture;
using Moq;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.UnitTests.Common;

namespace VSTranslations.Plugin.GoogleTranslate.UnitTests.AutoFixture.Customizations;

internal class GoogleTranslatorEngineCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Freeze<HttpResponseMessage>();

        var messageHandler = fixture.Freeze<Mock<HttpMessageDelegate>>();
        messageHandler
            .Setup(self => self(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(() => fixture.Create<HttpResponseMessage>());

        fixture.Register((HttpMessageHandler messageHandler, ITranslatorEngineConfig<GoogleTranslatorEngine> engineConfig) =>
            new GoogleTranslatorEngine(messageHandler, engineConfig));
    }
}