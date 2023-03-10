using VSTranslations.Plugin.GoogleTranslate.UnitTests.AutoFixture.Customizations;
using VSTranslations.UnitTests.AutoFixture.Attributes;

namespace VSTranslations.Plugin.GoogleTranslate.UnitTests.AutoFixture.Attributes;

public class GoogleTranslatorEngineAutoDataAttribute : AutoDataAttributeBase
{
    public GoogleTranslatorEngineAutoDataAttribute() : base(
        new HttpMessageHandlerCustomization(),
        new TranslatorEngineConfigCustomization(),
        new GoogleTranslatorEngineCustomization())
    {
    }
}
