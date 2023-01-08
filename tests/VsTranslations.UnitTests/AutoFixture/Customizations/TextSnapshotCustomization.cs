using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Text;
using Moq;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class TextSnapshotCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var span = fixture.Create<Span>();
            var textSnaphot = fixture.Freeze<Mock<ITextSnapshot>>();
            textSnaphot.Setup(self => self.Length).Returns(span.Length);

            textSnaphot
                .Setup(self => self.GetLineFromPosition(span.Start))
                .ReturnsUsingFixture(fixture);

            textSnaphot
                .Setup(self => self.GetLineFromPosition(span.End - 1))
                .ReturnsUsingFixture(fixture);

            textSnaphot
                .Setup(self => self.GetLineFromLineNumber(It.IsAny<int>()))
                .Returns<int>(lineNumber => fixture.Create<ITextSnapshotLine[]>()[lineNumber - 1]);
        }
    }
}
