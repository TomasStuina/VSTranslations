using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    public class TextViewAutoDataAttribute : AutoDataAttributeBase
    {
        public TextViewAutoDataAttribute() : base(new TextViewCustomization())
        {
        }
    }
}
