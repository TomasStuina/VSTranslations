using AutoFixture.Xunit2;

namespace VSTranslations.UnitTests.AutoFixture.Attributes
{
    public class SnapshotSpanInlineAutoDataAttribute : InlineAutoDataAttribute
    {
        public SnapshotSpanInlineAutoDataAttribute(params object[] values)
            : base(new SnapshotSpanAutoDataAttribute(), values)
        {
        }
    }
}
