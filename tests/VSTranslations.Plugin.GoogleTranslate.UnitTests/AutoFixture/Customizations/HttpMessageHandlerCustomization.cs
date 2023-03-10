using AutoFixture;
using Moq;
using System.Net;
using VSTranslations.UnitTests.Common;

namespace VSTranslations.Plugin.GoogleTranslate.UnitTests.AutoFixture.Customizations;

internal class HttpMessageHandlerCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<HttpResponseMessage>(composer => composer.With(responseMessage => responseMessage.StatusCode, HttpStatusCode.OK));
        fixture.Freeze<Mock<HttpMessageDelegate>>();
        fixture.Register((Mock<HttpMessageDelegate> messageHandlerMock) => messageHandlerMock.Object);
        fixture.Register<MockMessageHandler, HttpMessageHandler>((MockMessageHandler messageHandler) => messageHandler);
    }
}
