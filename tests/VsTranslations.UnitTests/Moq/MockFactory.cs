using Microsoft.VisualStudio.Text;
using Moq;

namespace VSTranslations.UnitTests.Moq
{
    internal static class MockFactory
    {
        public static Mock<ITextSnapshotLine> MockTextSnaphotLine(ITextSnapshot textSnapshot, int start, int end)
        {
            var textSnapshotLine = new Mock<ITextSnapshotLine>();

            textSnapshotLine.Setup(self => self.Start).Returns(() => new SnapshotPoint(textSnapshot, start));
            textSnapshotLine.Setup(self => self.End).Returns(() => new SnapshotPoint(textSnapshot, end));

            return textSnapshotLine;
        }
    }
}
