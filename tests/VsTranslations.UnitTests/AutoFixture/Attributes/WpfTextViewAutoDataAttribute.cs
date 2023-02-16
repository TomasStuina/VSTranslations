using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class WpfTextViewAutoDataAttribute : AutoDataAttributeBase
    {
        public WpfTextViewAutoDataAttribute() : base(
            new TextSnapshotCustomization(),
            new WpfTextViewCustomization())
        {
        }
    }
}
