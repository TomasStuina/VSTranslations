using AutoFixture;
using Microsoft.VisualStudio.Text;
using Moq;
using VSTranslations.Abstractions.Adornments;
using VSTranslations.Adornments;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    public class AdornmentCacheCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var addornmentCache = fixture.Freeze<Mock<IAdornmentCache<TranslatedTextAdornment>>>();

            addornmentCache.Setup(self => self.GetIntersectingSnapshotSpans(It.IsNotNull<NormalizedSnapshotSpanCollection>()))
                .Returns<NormalizedSnapshotSpanCollection>(snapShotCollection => snapShotCollection);
        }
    }
}
