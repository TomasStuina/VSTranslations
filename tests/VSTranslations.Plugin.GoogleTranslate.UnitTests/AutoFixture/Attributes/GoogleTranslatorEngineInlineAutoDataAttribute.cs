using AutoFixture.Xunit2;

namespace VSTranslations.Plugin.GoogleTranslate.UnitTests.AutoFixture.Attributes;

public class GoogleTranslatorEngineInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public GoogleTranslatorEngineInlineAutoDataAttribute(params object[] values)
        : base(new GoogleTranslatorEngineAutoDataAttribute(), values)
    {
    }
}
