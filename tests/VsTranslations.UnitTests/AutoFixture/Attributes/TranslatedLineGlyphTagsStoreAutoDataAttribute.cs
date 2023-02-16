using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class TranslatedLineGlyphTagsStoreAutoDataAttribute : AutoDataAttributeBase
    {
        public TranslatedLineGlyphTagsStoreAutoDataAttribute() : base(
            new TextSnapshotCustomization(),
            new TranslatedLineGlyphTagsStoreCustomization(),
            new TextLinesCollectionCustomization())
        {
        }
    }
}
