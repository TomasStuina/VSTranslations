using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using System.Collections.Generic;
using VSTranslations.Abstractions.Tagging;
using VSTranslations.Glyphs;
using VSTranslations.Services.Tagging;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class TranslatedLineGlyphTagsStoreCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<TranslatedLineGlyphTagsStore>();
            fixture.Freeze<IEnumerable<TranslatedLineGlyphTag>>();

            var tagStore = fixture.Freeze<Mock<ITranslatedLineGlyphTagsStore>>();
            tagStore.Setup(self => self.GetEnumerator()).ReturnsUsingFixture(fixture);
        }
    }
}
