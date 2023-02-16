using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;

namespace VSTranslations.UnitTests.Moq.Extensions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "VSTHRD010:Invoke single-threaded types on Main thread", Justification = "Mocks")]
    internal static class VsFontAndColorUtilitiesMockExtensions
    {
        public static Mock<IVsFontAndColorUtilities> MockGetColorType(this Mock<IVsFontAndColorUtilities> fontAndColorStorage,
            __VSCOLORTYPE colorType, int status = VSConstants.S_OK)
        {
            var colorTypeRaw = (int) colorType;
            var fontAndColorUtilities = fontAndColorStorage.As<IVsFontAndColorUtilities>();

            fontAndColorUtilities
                .Setup(self => self.GetColorType(It.IsAny<uint>(), out colorTypeRaw))
                .Returns(status);

            return fontAndColorUtilities;
        }
    }
}
