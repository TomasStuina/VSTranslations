using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Moq;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    public class WpfTextViewCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Freeze<PropertyCollection>();
            fixture.Freeze<Mock<IMappingPoint>>();
            fixture.Register((ITextSnapshot snapshot) => new VirtualSnapshotPoint(snapshot, 0));
            fixture.Register((VirtualSnapshotPoint bufferPosition, IMappingPoint mappingPoint, PositionAffinity positionAffinity) =>
                new CaretPosition(bufferPosition, mappingPoint, positionAffinity));

            var textView = fixture.Freeze<Mock<IWpfTextView>>();

            textView.Setup(self => self.Selection).ReturnsUsingFixture(fixture);
            textView.Setup(self => self.Caret.Position).ReturnsUsingFixture(fixture);
            textView.Setup(self => self.Selection.SelectedSpans).ReturnsUsingFixture(fixture);
            textView.Setup(self => self.TextBuffer).ReturnsUsingFixture(fixture);
            textView.Setup(self => self.TextBuffer.CurrentSnapshot).ReturnsUsingFixture(fixture);
            textView.Setup(self => self.Properties).ReturnsUsingFixture(fixture);
        }
    }
}
