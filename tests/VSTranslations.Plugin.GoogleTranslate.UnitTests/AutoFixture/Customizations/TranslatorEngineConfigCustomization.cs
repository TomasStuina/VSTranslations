using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Plugin.GoogleTranslate.UnitTests.AutoFixture.Customizations;

internal class TranslatorEngineConfigCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Language>(language => language.With(self => self.Code));

        var engineConfig = fixture.Freeze<Mock<ITranslatorEngineConfig<GoogleTranslatorEngine>>>();
        engineConfig.Setup(self => self.GetSourceLanguageAsync()).ReturnsUsingFixture(fixture);
        engineConfig.Setup(self => self.GetTargetLanguageAsync()).ReturnsUsingFixture(fixture);
    }
}