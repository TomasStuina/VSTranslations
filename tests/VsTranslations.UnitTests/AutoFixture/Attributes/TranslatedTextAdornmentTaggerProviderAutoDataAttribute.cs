using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class TranslatedTextAdornmentTaggerProviderAutoDataAttribute : AutoDataAttributeBase
    {
        public TranslatedTextAdornmentTaggerProviderAutoDataAttribute() : base(
            new TextSnapshotCustomization(),
            new WpfTextViewCustomization())
        {
        }
    }
}
