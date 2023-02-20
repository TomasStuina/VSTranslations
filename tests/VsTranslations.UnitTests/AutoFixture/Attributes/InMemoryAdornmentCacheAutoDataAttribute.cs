using VSTranslations.UnitTests.AutoFixture.Customizations;
using VSTranslations.Adornments;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class InMemoryAdornmentCacheAutoDataAttribute : AutoDataAttributeBase
    {
        public InMemoryAdornmentCacheAutoDataAttribute() : base(
            new TextSnapshotCustomization(),
            new WpfTextViewCustomization(),
            new TranslatedTextAdornmentCustomization(),
            new InMemoryAdornmentCacheCustomization())
        {
        }
    }
}
