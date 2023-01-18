using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Moq;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class WpfTextViewCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<Mock<IMappingPoint>>();
            fixture.Register((ITextSnapshot snapshot) => new VirtualSnapshotPoint(snapshot, 0));
            fixture.Register((VirtualSnapshotPoint bufferPosition, IMappingPoint mappingPoint, PositionAffinity positionAffinity) =>
                new CaretPosition(bufferPosition, mappingPoint, positionAffinity));

            var textview = fixture.Freeze<Mock<IWpfTextView>>();
            textview.Setup(self => self.Selection).ReturnsUsingFixture(fixture);
            textview.Setup(self => self.Caret.Position).ReturnsUsingFixture(fixture);
            textview.Setup(self => self.Selection.SelectedSpans).ReturnsUsingFixture(fixture);
        }
    }
}
