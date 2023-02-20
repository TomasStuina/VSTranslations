using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace VSTranslations.UnitTests.Moq.Extensions
{
    [SuppressMessage("Usage", "VSTHRD010:Invoke single-threaded types on Main thread", Justification = "Mocks")]
    internal static class VsFontAndColorStorageMockExtensions
    {
        public static Mock<IVsFontAndColorStorage> MockGetItem(this Mock<IVsFontAndColorStorage> fontAndColorStorage,
            string name, uint background, uint foreground, int status = VSConstants.S_OK)
        {
            fontAndColorStorage
                .Setup(self => self.GetItem(name, It.Is<ColorableItemInfo[]>(info => info.Length == 1)))
                .Returns<string, ColorableItemInfo[]>((name, info) =>
                {
                    info[0].crBackground = background;
                    info[0].crForeground = foreground;

                    return status;
                });

            return fontAndColorStorage;
        }

        public static Mock<IVsFontAndColorStorage> MockGetFont(this Mock<IVsFontAndColorStorage> fontAndColorStorage,
            string fontFamilyName, ushort pointSize, int status = VSConstants.S_OK)
        {
            fontAndColorStorage
                .Setup(self => self.GetFont(
                    It.Is<LOGFONTW[]>(logFontw => logFontw.Length == 1),
                    It.Is<FontInfo[]>(fontInfo => fontInfo.Length == 1)))
                .Returns<LOGFONTW[], FontInfo[]>((logFontw, fontInfo) =>
                {
                    fontInfo[0].bstrFaceName = fontFamilyName;
                    fontInfo[0].wPointSize = pointSize;
                    return status;
                });

            return fontAndColorStorage;
        }
    }
}
