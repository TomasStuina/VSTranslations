using VSTranslations.UnitTests.AutoFixture.Customizations;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
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
