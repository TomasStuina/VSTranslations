using AutoFixture;
using Microsoft.VisualStudio.Text;
using Moq;
using VsTranslations.UnitTests.Common.Moq.Extensions;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class TextSnapshotLineCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var textSnapshotLine = fixture.Freeze<Mock<ITextSnapshotLine>>();
            textSnapshotLine.SetupSequence(self => self.LineNumber).ReturnsMany(1, 3);
        }
    }
}
