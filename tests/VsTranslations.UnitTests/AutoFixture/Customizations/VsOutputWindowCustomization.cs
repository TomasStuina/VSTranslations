using AutoFixture;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using System;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

internal class VsOutputWindowCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var outputWindowPaneMock = fixture.Freeze<Mock<IVsOutputWindowPane>>();
        var outputWindowPaneNoPumpMock = outputWindowPaneMock.As<IVsOutputWindowPaneNoPump>();
        var outputWindowPane = outputWindowPaneMock.Object;

        var outputWindow = fixture.Freeze<Mock<IVsOutputWindow>>();
        outputWindow.Setup(self => self.GetPane(ref It.Ref<Guid>.IsAny, out outputWindowPane)).Returns(0);

        fixture.Inject(outputWindowPaneNoPumpMock);
    }
}
