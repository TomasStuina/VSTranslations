using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class TranslatedLineGlyphTaggerProviderAutoDataAttribute : AutoDataAttributeBase
    {
        public TranslatedLineGlyphTaggerProviderAutoDataAttribute() : base(
            new TextViewCustomization())
        {
        }
    }
}
