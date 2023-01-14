using AutoFixture;
using Microsoft.VisualStudio.Text;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class NormalizedSnapshotSpanCollectionCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register((ITextSnapshot textSnapshot, Span span) => {
                return new NormalizedSnapshotSpanCollection(textSnapshot, span);

            }
            );
        }
    }
}
