using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Text;
using Moq;
using VsTranslations.UnitTests.Common.Moq.Extensions;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class TextSnapshotCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            CustomizeSnapshotSpan(fixture);
            CustomizeTextSnapshotLine(fixture);
            CustomizeTextSnaphot(fixture);
            CustomizeTrackingSpan(fixture);
            CustomizeNormalizedSnapshotSpanCollection(fixture);
        }

        private void CustomizeSnapshotSpan(IFixture fixture)
        {
            fixture.Register(() => new Span(0, 100));
            fixture.Register((ITextSnapshot textSnapshot, Span span) => new SnapshotSpan(textSnapshot, span));
        }

        private void CustomizeTextSnapshotLine(IFixture fixture)
        {
            var textSnapshotLine = fixture.Freeze<Mock<ITextSnapshotLine>>();
            textSnapshotLine.SetupSequence(self => self.LineNumber).ReturnsMany(1, 3);
        }

        private void CustomizeTextSnaphot(IFixture fixture)
        {
            var textSnaphot = fixture.Freeze<Mock<ITextSnapshot>>();
            textSnaphot.Setup(self => self.Length).Returns(int.MaxValue);

            textSnaphot
                .Setup(self => self.GetLineFromPosition(It.IsAny<int>()))
                .ReturnsUsingFixture(fixture);

            textSnaphot
                .Setup(self => self.GetLineFromLineNumber(It.IsAny<int>()))
                .Returns<int>(lineNumber => fixture.Create<ITextSnapshotLine[]>()[lineNumber - 1]);

            textSnaphot
                .Setup(self => self.GetText(It.IsAny<Span>()))
                .ReturnsUsingFixture(fixture);

            textSnaphot
                .Setup(self => self.CreateTrackingSpan(It.IsAny<Span>(), SpanTrackingMode.EdgeExclusive))
                .ReturnsUsingFixture(fixture);
        }

        private void CustomizeTrackingSpan(IFixture fixture)
        {
            var trackingSpan = fixture.Freeze<Mock<ITrackingSpan>>();
            trackingSpan.Setup(self => self.GetSpan(It.IsNotNull<ITextSnapshot>())).ReturnsUsingFixture(fixture);
        }

        private void CustomizeNormalizedSnapshotSpanCollection(IFixture fixture)
        {
            fixture.Register((ITextSnapshot textSnapshot, Span span) => new NormalizedSnapshotSpanCollection(textSnapshot, span));
        }
    }
}
