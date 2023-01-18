using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Moq;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class TextViewCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<PropertyCollection>();

            var textView = fixture.Freeze<Mock<ITextView>>();
            textView.Setup(self => self.Properties).ReturnsUsingFixture(fixture);
        }
    }
}
