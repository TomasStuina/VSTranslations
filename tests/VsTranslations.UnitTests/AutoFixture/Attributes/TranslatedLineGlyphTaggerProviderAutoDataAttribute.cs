using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    public class TranslatedLineGlyphTaggerProviderAutoDataAttribute : AutoDataAttributeBase
    {
        public TranslatedLineGlyphTaggerProviderAutoDataAttribute() : base(
            new TextViewCustomization())
        {
        }
    }
}
