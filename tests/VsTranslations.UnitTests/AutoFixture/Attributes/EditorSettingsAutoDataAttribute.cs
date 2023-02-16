using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class EditorSettingsAutoDataAttribute : AutoDataAttributeBase
    {
        public EditorSettingsAutoDataAttribute() : base(
            new TextSnapshotCustomization(),
            new WpfTextViewCustomization(),
            new TranslatedTextAdornmentCustomization(),
            new VsFontAndColorStorageCustomization())
        {
        }
    }
}
