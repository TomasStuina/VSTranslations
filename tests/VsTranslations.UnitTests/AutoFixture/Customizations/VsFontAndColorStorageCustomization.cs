using AutoFixture;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using System.Drawing.Text;
using System.Windows.Media;
using VSTranslations.UnitTests.Moq.Extensions;

namespace VSTranslations.UnitTests.AutoFixture.Customizations
{
    internal class VsFontAndColorStorageCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            const string fontFamilyName = nameof(GenericFontFamilies.Monospace);
            fixture.Register(() => new FontFamily(fontFamilyName));

            var fontAndColorStorage = fixture.Freeze<Mock<IVsFontAndColorStorage>>();
            fontAndColorStorage.MockGetFont(fontFamilyName, pointSize: 9);
            fontAndColorStorage.MockGetItem("Comment", background: Colors.White.ToRgba(), foreground: Colors.Black.ToRgba());

            var fontAndColorUtilities = fontAndColorStorage.As<IVsFontAndColorUtilities>();
            fontAndColorUtilities.MockGetColorType(__VSCOLORTYPE.CT_RAW);

            fixture.Inject(fontAndColorUtilities);
        }
    }
}
