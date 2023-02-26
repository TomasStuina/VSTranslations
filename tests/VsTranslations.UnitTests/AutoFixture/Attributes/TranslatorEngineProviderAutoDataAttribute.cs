using VSTranslations.UnitTests.AutoFixture.Customizations;
using VSTranslations.UnitTests.Common.AutoFixture.Attributes;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

internal class TranslatorEngineProviderAutoDataAttribute : AutoDataAttributeBase
{
    public TranslatorEngineProviderAutoDataAttribute() : base(
        AutoDataConfiguration.AutoConfigureMocks,
        new VsSettingsManagerCustomization(),
        new TranslatorEngineProviderCustomization())
    {
    }
}