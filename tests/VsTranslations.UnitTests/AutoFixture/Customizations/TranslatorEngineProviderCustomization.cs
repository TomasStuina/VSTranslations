using AutoFixture;
using Moq;
using System;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

internal class TranslatorEngineProviderCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Freeze<Mock<ITranslatorEngineProvider>>();
        fixture.Register((ITranslatorEngine engine, ITranslatorEngineMetadata metadata) =>
            new Lazy<ITranslatorEngine, ITranslatorEngineMetadata>(() => engine, metadata));
    }
}
