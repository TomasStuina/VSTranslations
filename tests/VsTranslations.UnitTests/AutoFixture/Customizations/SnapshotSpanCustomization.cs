using AutoFixture;
using Microsoft.VisualStudio.Text;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class SnapshotSpanCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(() => new Span(0, 100));
            fixture.Register((ITextSnapshot textSnapshot, Span span) => new SnapshotSpan(textSnapshot, span));
        }
    }
}
