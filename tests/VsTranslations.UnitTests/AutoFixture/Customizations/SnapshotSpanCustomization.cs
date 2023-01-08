using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.GraphModel.CodeSchema;
using Microsoft.VisualStudio.Text;
using Moq;
using System;
using System.Collections.Generic;

namespace VsTranslations.UnitTests.AutoFixture.Customizations
{
    public class SnapshotSpanCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var span = new Span(0, 100);

            fixture.Register(() => span);
            fixture.Register((ITextSnapshot textSnapshot, Span span) => new SnapshotSpan(textSnapshot, span));

            var textSnapshotLine = fixture.Freeze<Mock<ITextSnapshotLine>>();
            textSnapshotLine.SetupSequence(self => self.LineNumber)
                .Returns(1)
                .Returns(3);
        }
    }
}
