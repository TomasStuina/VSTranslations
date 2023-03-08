using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using System;
using System.Threading.Tasks;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Translating;
using VSTranslations.Services.Translating;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

internal class TranslatorEngineConfigManagerCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var engineConfig = fixture.Freeze<Mock<ITranslatorEngineConfig>>();
        engineConfig.Setup(self => self.GetLanguagesAsync()).ReturnsUsingFixture(fixture);
        engineConfig.Setup(self => self.GetSourceLanguageAsync()).ReturnsUsingFixture(fixture);
        engineConfig.Setup(self => self.GetTargetLanguageAsync()).ReturnsUsingFixture(fixture);

        var engine = fixture.Freeze<Mock<ITranslatorEngine>>();
        engine.Setup(self => self.TranslatorEngineConfig).ReturnsUsingFixture(fixture);

        var engineProvider = fixture.Freeze<Mock<ITranslatorEngineProvider>>();
        engineProvider.Setup(self => self.GetAsync()).ReturnsUsingFixture(fixture);

        var factory = fixture.Freeze<Mock<Func<Task<ITranslatorEngineProvider>>>>();
        factory.Setup(self => self()).ReturnsUsingFixture(fixture);

        fixture.Register((Mock<Func<Task<ITranslatorEngineProvider>>> factory) => factory.Object);
        fixture.Register((Func<Task<ITranslatorEngineProvider>> factory) => new TranslatorEngineConfigManager(factory));
    }
}
