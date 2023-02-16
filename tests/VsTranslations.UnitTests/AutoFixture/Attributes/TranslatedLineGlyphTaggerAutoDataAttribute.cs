using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class TranslatedLineGlyphTaggerAutoDataAttribute : AutoDataAttributeBase
    {
        public TranslatedLineGlyphTaggerAutoDataAttribute() : base(
            new TextSnapshotCustomization(),
            new TextViewCustomization(),
            new TranslatedLineGlyphTagsStoreCustomization())
        {
        }
    }
}
