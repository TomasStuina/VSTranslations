using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    public class WpfTextViewAutoDataAttribute : AutoDataAttributeBase
    {
        public WpfTextViewAutoDataAttribute()
            : base(new SnapshotSpanCustomization(), new TextSnapshotLineCustomization(), new TextSnapshotCustomization(),
                   new NormalizedSnapshotSpanCollectionCustomization(), new WpfTextViewCustomization())
        {
        }
    }
}
