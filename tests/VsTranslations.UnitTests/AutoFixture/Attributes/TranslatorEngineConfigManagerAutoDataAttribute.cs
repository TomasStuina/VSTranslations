using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

public class TranslatorEngineConfigManagerAutoDataAttribute : AutoDataAttributeBase
{
    public TranslatorEngineConfigManagerAutoDataAttribute() : base(
        new LanguageCustomization(),
        new TranslatorEngineConfigManagerCustomization())
    {
    }
}
