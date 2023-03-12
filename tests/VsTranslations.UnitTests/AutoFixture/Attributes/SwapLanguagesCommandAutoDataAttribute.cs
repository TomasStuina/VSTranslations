using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes;

public class SwapLanguagesCommandAutoDataAttribute : AutoDataAttributeBase
{
    public SwapLanguagesCommandAutoDataAttribute() : base(
        new TranslatorEngineConfigManagerCustomization(),
        new OleMenuCmdEventArgsCustomization())
    {
    }
}
