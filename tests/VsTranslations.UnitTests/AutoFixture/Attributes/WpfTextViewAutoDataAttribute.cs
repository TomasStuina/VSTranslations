using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
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
