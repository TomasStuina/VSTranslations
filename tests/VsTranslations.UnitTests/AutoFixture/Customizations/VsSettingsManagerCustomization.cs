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
        fixture.Inject(Mock.Of<IVsWritableSettingsStore>());

        var settingsManager = new Mock<IVsSettingsManager>();
        settingsManager.Setup(self => self.GetReadOnlySettingsStore(It.IsAny<uint>(), out settingsStore)).Returns(0);
        fixture.Inject(settingsManager);
    }
}