using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using System;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

internal class VsUIShellCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var inforBarUIElement = fixture.Freeze<Mock<IVsInfoBarUIElement>>();
        var infoBarUIFactoryMock = fixture.Freeze<Mock<IVsInfoBarUIFactory>>();
        infoBarUIFactoryMock
            .Setup(self => self.CreateInfoBar(It.IsAny<IVsInfoBar>()))
            .ReturnsUsingFixture(fixture);

        var infoBarHostMock = fixture.Freeze<Mock<IVsInfoBarHost>>();
        var infoBarHost = infoBarHostMock.Object as object;

        var windowFrameMock = fixture.Freeze<Mock<IVsWindowFrame>>();
        windowFrameMock
            .Setup(self => self.GetProperty((int)__VSFPROPID7.VSFPROPID_InfoBarHost, out infoBarHost))
            .Returns(0);

        var windowFrame = windowFrameMock.Object;
        var uiShell = fixture.Freeze<Mock<IVsUIShell>>();
        uiShell
            .Setup(self => self.FindToolWindow((uint)__VSFINDTOOLWIN.FTW_fForceCreate, ref It.Ref<Guid>.IsAny, out windowFrame))
            .Returns(0);
    }
}