using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Moq;
using VSTranslations.Glyphs;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    public class ViewTagAggregatorFactoryServiceCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<Mock<ITagAggregator<TranslatedLineGlyphTag>>>();

            var tagAggregatorFacotrySerivice = fixture.Freeze<Mock<IViewTagAggregatorFactoryService>>();
            tagAggregatorFacotrySerivice.Setup(self => self.CreateTagAggregator<TranslatedLineGlyphTag>(It.IsNotNull<ITextView>()))
                .ReturnsUsingFixture(fixture);
        }
    }
}
