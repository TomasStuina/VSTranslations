using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    public class SnapshotSpanAutoDataAttribute : AutoDataAttributeBase
    {
        public SnapshotSpanAutoDataAttribute() : base(new TextSnapshotCustomization())
        { 
        }
    }
}
