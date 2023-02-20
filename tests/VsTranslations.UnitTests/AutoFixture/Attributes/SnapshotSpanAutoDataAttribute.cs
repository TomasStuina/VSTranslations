using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class SnapshotSpanAutoDataAttribute : AutoDataAttributeBase
    {
        public SnapshotSpanAutoDataAttribute() : base(new TextSnapshotCustomization())
        { 
        }
    }
}
