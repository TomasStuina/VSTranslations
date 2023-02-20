using AutoFixture;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Threading.Tasks;
using VSTranslations.Services.Settings;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    internal class EditorSettingsFactoryCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register((IVsFontAndColorStorage storage) => new Func<Task<IVsFontAndColorStorage>>(() => Task.FromResult(storage)));
            fixture.Register((Func<Task<IVsFontAndColorStorage>> storageFactory) => new EditorSettingsFactory(storageFactory));
        }
    }
}
