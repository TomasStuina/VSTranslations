using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Moq;
using VSTranslations.Glyphs;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    public class TranslatedLineGlyphTagAggregatorCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<TranslatedLineGlyphTag>();

            var mappingTagSpan = fixture.Freeze<Mock<IMappingTagSpan<TranslatedLineGlyphTag>>>();
            mappingTagSpan
                .Setup(self => self.Tag)
                .ReturnsUsingFixture(fixture);

            mappingTagSpan
                .Setup(self => self.Span.GetSpans(It.IsNotNull<ITextSnapshot>()))
                .ReturnsUsingFixture(fixture);

            var tagger = fixture.Freeze<Mock<ITagAggregator<TranslatedLineGlyphTag>>>();
            tagger
                .Setup(self => self.GetTags(It.IsNotNull<NormalizedSnapshotSpanCollection>()))
                .ReturnsUsingFixture(fixture);
        }
    }
}
