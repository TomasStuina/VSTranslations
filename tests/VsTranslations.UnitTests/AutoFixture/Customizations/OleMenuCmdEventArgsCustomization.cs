using AutoFixture;
using Microsoft.VisualStudio.Shell;
using System;

namespace VSTranslations.UnitTests.AutoFixture.Customizations;

internal class OleMenuCmdEventArgsCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new OleMenuCmdEventArgs(null, IntPtr.Zero));
    }
}
