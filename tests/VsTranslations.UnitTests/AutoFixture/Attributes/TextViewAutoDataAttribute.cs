using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class TextViewAutoDataAttribute : AutoDataAttributeBase
    {
        public TextViewAutoDataAttribute() : base(new TextViewCustomization())
        {
        }
    }
}
