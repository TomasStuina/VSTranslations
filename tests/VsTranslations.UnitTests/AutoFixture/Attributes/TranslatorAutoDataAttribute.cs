using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    public class TranslatorAutoDataAttribute : AutoDataAttributeBase
    {
        public TranslatorAutoDataAttribute()
            : base(new SnapshotSpanCustomization(), new TextSnapshotCustomization(),
                   new TranslatorCustomization(), new TranslatorEngineCustomization())
        {
        }
    }
}
