using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    public class TranslatedLineGlyphTagsStoreAutoDataAttribute : AutoDataAttributeBase
    {
        public TranslatedLineGlyphTagsStoreAutoDataAttribute()
            : base(new SnapshotSpanCustomization(), new TextSnapshotCustomization(), 
                  new TranslatedLineGlyphTagsStoreCustomization(), new TextLinesCollectionCustomization())
        {
        }
    }
}
