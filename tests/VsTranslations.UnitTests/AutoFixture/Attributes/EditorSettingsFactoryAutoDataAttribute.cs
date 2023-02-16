using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class EditorSettingsFactoryAutoDataAttribute : AutoDataAttributeBase
    {
        public EditorSettingsFactoryAutoDataAttribute() : base(
            new VsFontAndColorStorageCustomization(),
            new EditorSettingsFactoryCustomization(),
            new WpfTextViewCustomization())
        {
        }
    }
}
