using Microsoft.VisualStudio.Shell.Interop;
using System.Windows.Media;

namespace VSTranslations
{
    internal static class Constants
    {
        public const string VsTextMarker = "VsTextMarker";

        public static class Editor
        {
            public const uint ReadonlyCategoryFlags = (uint)(__FCSTORAGEFLAGS.FCSF_READONLY | __FCSTORAGEFLAGS.FCSF_LOADDEFAULTS | __FCSTORAGEFLAGS.FCSF_NOAUTOCOLORS);
            public const double DefaultFontSize = 10.0d;
            public const string CommentItem = "Comment";

            public static readonly Guid TextEditorGuid = new(FontsAndColorsCategory.TextEditor);

            public static readonly Color DefaultForegroundColor = Color.FromArgb(127, 170, 170, 170);
            public static readonly Color DefaultBackgroundColor = Colors.White;

            public static readonly FontFamily DefaultFontFamily = new("Consolas");
        }
    }
}
