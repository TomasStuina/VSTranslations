using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using Moq;
using System;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    public class WpfTextViewCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var propertyCollection = fixture.Freeze<PropertyCollection>();
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
            textView.Setup(self => self.TextBuffer.Properties).ReturnsUsingFixture(fixture);
            textView.Setup(self => self.Properties).ReturnsUsingFixture(fixture);

            var bufferAdapter = fixture.Freeze<Mock<IVsTextBuffer>>();
            var persistFileFormat = bufferAdapter.As<IPersistFileFormat>();
            var fileName = fixture.Create<Guid>().ToString();
            var formatIndex = fixture.Create<uint>();

            persistFileFormat.Setup(self => self.GetCurFile(out fileName, out formatIndex)).Returns(0);
            propertyCollection[typeof(IVsTextBuffer)] = bufferAdapter.Object;
        }
    }
}
