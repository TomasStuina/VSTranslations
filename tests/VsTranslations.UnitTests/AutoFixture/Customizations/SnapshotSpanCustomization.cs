using AutoFixture;
using Microsoft.VisualStudio.Text;
using Moq;
using VsTranslations.UnitTests.Common.Moq.Extensions;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class SnapshotSpanCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() => new Span(0, 100));
            fixture.Register((ITextSnapshot textSnapshot, Span span) => new SnapshotSpan(textSnapshot, span));

            var textSnapshotLine = fixture.Freeze<Mock<ITextSnapshotLine>>();
            textSnapshotLine.SetupSequence(self => self.LineNumber).ReturnsMany(1, 3);
        }
    }
}
