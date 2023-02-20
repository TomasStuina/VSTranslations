using AutoFixture;
using VSTranslations.Adornments;
using VSTranslations.Glyphs;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    internal class TranslatedTextAdornmentCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register((TranslatedLineGlyphTag glyphTag) => new TranslatedTextAdornment(glyphTag));
        }
    }
}
