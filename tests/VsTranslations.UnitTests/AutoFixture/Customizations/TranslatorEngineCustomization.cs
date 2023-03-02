using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using System.Threading.Tasks;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

public class TranslatorEngineCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var translatorEngine = fixture.Freeze<Mock<ITranslatorEngine>>();
        translatorEngine
            .Setup(self => self.TranslateAsync(It.IsAny<string>()))
            .Returns<string>(text => Task.FromResult(text));

        var translatorEngineProvider = fixture.Freeze<Mock<ITranslatorEngineProvider>>();
        translatorEngineProvider
            .Setup(self => self.GetAsync())
            .ReturnsUsingFixture(fixture);
    }
}
