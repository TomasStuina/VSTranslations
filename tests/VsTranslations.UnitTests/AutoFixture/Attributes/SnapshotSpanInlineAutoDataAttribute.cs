using AutoFixture.Xunit2;

namespace VsTranslations.UnitTests.AutoFixture.Attributes
{
    public class SnapshotSpanInlineAutoDataAttribute : InlineAutoDataAttribute
    {
        public SnapshotSpanInlineAutoDataAttribute(params object[] values)
            : base(new SnapshotSpanAutoDataAttribute(), values)
        {
        }
    }
}
