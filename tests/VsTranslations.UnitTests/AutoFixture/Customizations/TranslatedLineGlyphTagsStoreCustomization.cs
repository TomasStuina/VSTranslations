using AutoFixture;
using Moq;
using VSTranslations.Abstractions.Tagging;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class TranslatedLineGlyphTagsStoreCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<Mock<ITranslatedLineGlyphTagsStore>>();
        }
    }
}
