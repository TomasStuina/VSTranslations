using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
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
