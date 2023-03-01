using VSTranslations.UnitTests.AutoFixture.Customizations;
using VSTranslations.UnitTests.Common.AutoFixture.Attributes;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

internal class TranslatorEngineProviderAutoDataAttribute : AutoDataAttributeBase
{
    public TranslatorEngineProviderAutoDataAttribute() : this(AutoDataConfiguration.AutoConfigureMocks)
    {
    }

    public TranslatorEngineProviderAutoDataAttribute(AutoDataConfiguration configuration) : base(
        configuration,
        new VsSettingsManagerCustomization(),
        new TranslatorEngineProviderCustomization())
    {
    }
}