using VsTranslations.UnitTests.AutoFixture.Customizations;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    public class TranslatorAutoDataAttribute : AutoDataAttributeBase
    {
        public TranslatorAutoDataAttribute() : base(
            new TextSnapshotCustomization(),
            new TranslatorEngineCustomization())
        {
        }
    }
}
