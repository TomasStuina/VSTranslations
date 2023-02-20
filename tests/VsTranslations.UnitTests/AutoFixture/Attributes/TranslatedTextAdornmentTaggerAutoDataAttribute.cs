using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class TranslatedTextAdornmentTaggerAutoDataAttribute : AutoDataAttributeBase
    {
        public TranslatedTextAdornmentTaggerAutoDataAttribute() : base(
            new TextSnapshotCustomization(),
            new WpfTextViewCustomization(),
            new AdornmentCacheCustomization(),
            new TranslatedLineGlyphTagAggregatorCustomization(),
            new EditorSettingsCustomization())
        {
        }
    }
}
