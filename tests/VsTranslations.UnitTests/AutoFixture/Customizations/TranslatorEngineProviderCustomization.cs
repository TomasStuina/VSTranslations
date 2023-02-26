using AutoFixture;
using System;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

internal class TranslatorEngineProviderCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register((ITranslatorEngine engine, ITranslatorEngineMetadata metadata) =>
            new Lazy<ITranslatorEngine, ITranslatorEngineMetadata>(() => engine, metadata));
    }
}
