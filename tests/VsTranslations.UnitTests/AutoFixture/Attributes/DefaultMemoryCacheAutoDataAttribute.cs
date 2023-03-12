using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

public class DefaultMemoryCacheAutoDataAttribute : AutoDataAttributeBase
{
    public DefaultMemoryCacheAutoDataAttribute() : base(
        new VsSettingsManagerCustomization(),
        new DefaultMemoryCacheCustomization())
    {
    }
}