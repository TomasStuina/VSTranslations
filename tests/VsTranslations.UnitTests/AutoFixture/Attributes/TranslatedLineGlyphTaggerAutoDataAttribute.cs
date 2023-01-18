using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
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
