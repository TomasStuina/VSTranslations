using AutoFixture;
using AutoFixture.AutoMoq;
using EnvDTE;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Moq;
using System;

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
