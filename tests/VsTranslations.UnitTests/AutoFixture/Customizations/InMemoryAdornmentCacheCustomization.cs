using AutoFixture;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Moq;
using VSTranslations.Adornments;
using VSTranslations.Delegates;
using VSTranslations.Services.Adornments;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    internal class InMemoryAdornmentCacheCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var spanTranslate = fixture.Freeze<Mock<SpanTranslateDelegate>>();
            spanTranslate
                .Setup(self => self(It.IsAny<SnapshotSpan>(), It.IsAny<ITextSnapshot>()))
                .Returns<SnapshotSpan, ITextSnapshot>((snapshotSpan, _) => snapshotSpan);

            fixture.Inject(spanTranslate.Object);

            fixture.Register((IWpfTextView view, SpanTranslateDelegate spanTranslate) =>
                new InMemoryAdornmentCache<TranslatedTextAdornment>(view, spanTranslate));
        }
    }
}
