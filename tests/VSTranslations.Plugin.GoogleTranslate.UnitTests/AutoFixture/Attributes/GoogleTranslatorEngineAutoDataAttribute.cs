using VSTranslations.Plugin.GoogleTranslate.UnitTests.AutoFixture.Customizations;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.UnitTests.Common.AutoFixture.Customizations;

namespace VSTranslations.Plugin.GoogleTranslate.UnitTests.AutoFixture.Attributes;

public class GoogleTranslatorEngineAutoDataAttribute : AutoDataAttributeBase
{
    public GoogleTranslatorEngineAutoDataAttribute() : base(
        new CacheCustomization(),
        new CacheFactoryCustomization(),
        new HttpMessageHandlerCustomization(),
        new TranslatorEngineConfigCustomization(),
        new GoogleTranslatorEngineCustomization())
    {
    }
}
