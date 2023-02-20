using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using VSTranslations.UnitTests.AutoFixture.Attributes;
using VSTranslations.Glyphs;
using VSTranslations.Services.Tagging;
using Xunit;

namespace VSTranslations.UnitTests.Services.Tagging
{
    public class TranslatedLineGlyphFactoryProviderTests
    {
        [UITheory]
        [DefaultAutoData]
        public void GetGlyphFactory_WhenInvoked_ShouldReturnFactoryForCreatingTranslatedLineGlyphTagView(IFixture fixture,
            IWpfTextView wpfTextView, IWpfTextViewMargin wpfTextViewMargin, IWpfTextViewLine wpfTextViewLine, IGlyphTag glyphTag)
        {
            // Arrange
            var factoryProvider = fixture.Create<TranslatedLineGlyphFactoryProvider>();

            // Act
            var glyphFactory = factoryProvider.GetGlyphFactory(wpfTextView, wpfTextViewMargin);

            // Assert
            glyphFactory.Should().NotBeNull();
            glyphFactory.GenerateGlyph(wpfTextViewLine, glyphTag).Should().BeOfType<TranslatedLineGlyphTagView>();
        }
    }
}
