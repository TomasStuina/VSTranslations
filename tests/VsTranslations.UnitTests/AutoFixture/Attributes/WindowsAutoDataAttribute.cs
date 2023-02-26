using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class WindowsAutoDataAttribute : AutoDataAttributeBase
    {
        public WindowsAutoDataAttribute() : base(new VsOutputWindowCustomization())
        {
        }
    }
}
