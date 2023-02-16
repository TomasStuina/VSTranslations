using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Moq;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    public class TextViewCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<PropertyCollection>();
            fixture.Freeze<Mock<ITextBuffer>>();

            var textView = fixture.Freeze<Mock<ITextView>>();

            textView.Setup(self => self.Properties).ReturnsUsingFixture(fixture);
            textView.Setup(self => self.TextBuffer).ReturnsUsingFixture(fixture);
        }
    }
}
