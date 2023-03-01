using AutoFixture;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

internal class VsSettingsManagerCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var settingsStore = Mock.Of<IVsSettingsStore>();
        fixture.Inject(settingsStore);
        var writableSettingsStoreMock = new Mock<IVsWritableSettingsStore>();
        fixture.Inject(writableSettingsStoreMock);
        var writableSettingsStore = writableSettingsStoreMock.Object;

        var settingsManager = new Mock<IVsSettingsManager>();
        settingsManager.Setup(self => self.GetReadOnlySettingsStore(It.IsAny<uint>(), out settingsStore)).Returns(0);
        settingsManager.Setup(self => self.GetWritableSettingsStore(It.IsAny<uint>(), out writableSettingsStore)).Returns(0);
        fixture.Inject(settingsManager);
    }
}