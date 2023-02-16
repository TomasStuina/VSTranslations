using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows;
using System.Windows.Media;
using VSTranslations.Abstractions.Settings;
using VSTranslations.Extensions;
using VSTranslations.Common.Extensions;
using static VSTranslations.Constants.Editor;

namespace VSTranslations.Services.Settings
{
    /// <summary>
    /// A concrete implementation of <see cref="IEditorSettings"/>
    /// that wraps <see cref="IVsFontAndColorStorage"/> to get the data.
    /// </summary>
    internal class EditorSettings : IEditorSettings
    {
        private readonly FontSizeConverter _fontSizeConverter;
        private readonly IVsFontAndColorStorage _fontAndColorStorage;

        public EditorSettings(IVsFontAndColorStorage fontAndColorStorage)
        {
            _fontAndColorStorage = fontAndColorStorage.ThrowIfNull(nameof(fontAndColorStorage));
            _fontSizeConverter = new FontSizeConverter();
            LoadEditorSettings();
        }

        /// <inheritdoc/>
        public Brush CommentForegroundColor { get; private set; }

        /// <inheritdoc/>
        public Brush CommentBackgroundColor { get; private set; }

        /// <inheritdoc/>
        public FontFamily FontFamily { get; private set; }

        /// <inheritdoc/>
        public double FontSize { get; private set; }

        /// <summary>
        /// Loads all the settings from <see cref="IVsFontAndColorStorage"/>.
        /// </summary>
        public void LoadEditorSettings()
        {
            ThreadHelper.JoinableTaskFactory.Run(() => LoadEditorSettingsAsync());
        }

        /// <summary>
        /// Asynchroniously loads all the settings from <see cref="IVsFontAndColorStorage"/>.
        /// </summary>
        /// <returns>A task indicating the completion.</returns>
        public async Task LoadEditorSettingsAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            try
            {
                if (!TryOpenTextEditorCategoryAsReadOnly(_fontAndColorStorage))
                {
                    return;
                }

                (FontSize, FontFamily) = GetFontInfo();
                (CommentBackgroundColor, CommentForegroundColor) = GetColorBrushes(CommentItem);
            }
            finally
            {
                _fontAndColorStorage.CloseCategory();
            }
        }

        private bool TryOpenTextEditorCategoryAsReadOnly(IVsFontAndColorStorage fontAndColorStorage)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            return fontAndColorStorage.OpenCategory(TextEditorGuid, ReadonlyCategoryFlags) == VSConstants.S_OK;
        }

        private (double FontSize, FontFamily FontFamily) GetFontInfo()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var logFontw = new LOGFONTW[1];
            var fontInfo = new FontInfo[1];
            if (_fontAndColorStorage.GetFont(logFontw, fontInfo) == VSConstants.S_OK)
            {
                var fontSize = _fontSizeConverter.ConvertFromPointSize(fontInfo[0].wPointSize);
                var fontFamily = new FontFamily(fontInfo[0].bstrFaceName);

                return (fontSize, fontFamily);
            }

            return (DefaultFontSize, DefaultFontFamily);
        }

        private (Brush Background, Brush Foreground) GetColorBrushes(string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var (backgroundColor, foregroundColor) = GetColors(name);

            return (new SolidColorBrush(backgroundColor), new SolidColorBrush(foregroundColor));
        }

        private (Color Background, Color Foreground) GetColors(string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (!TryGetColorableItemInfo(_fontAndColorStorage, name, out var colorableItemInfo))
            {
                return (DefaultBackgroundColor, DefaultForegroundColor);
            }

            return (GetBackgroundColor(_fontAndColorStorage, colorableItemInfo), GetForegroundColor(_fontAndColorStorage, colorableItemInfo));
        }

        private bool TryGetColorableItemInfo(IVsFontAndColorStorage fontAndColorStorage, string name, out ColorableItemInfo colorableItemInfo)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var colorItems = new ColorableItemInfo[1];
            if (fontAndColorStorage.GetItem(name, colorItems) != VSConstants.S_OK)
            {
                colorableItemInfo = default;
                return false;
            }

            colorableItemInfo = colorItems[0];
            return true;
        }

        private Color GetBackgroundColor(IVsFontAndColorStorage fontAndColorStorage, ColorableItemInfo colorableItemInfo)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            return GetColor(fontAndColorStorage, colorableItemInfo.crBackground, DefaultBackgroundColor);
        }


        private Color GetForegroundColor(IVsFontAndColorStorage fontAndColorStorage, ColorableItemInfo colorableItemInfo)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            return GetColor(fontAndColorStorage, colorableItemInfo.crForeground, DefaultForegroundColor);
        }

        private Color GetColor(IVsFontAndColorStorage fontAndColorStorage, uint colorReference, Color fallbackColor)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var fontAndColorUtilities = (IVsFontAndColorUtilities)fontAndColorStorage;
            if (fontAndColorUtilities.GetColorType(colorReference, out var colorType) != VSConstants.S_OK
                || colorType != (int)__VSCOLORTYPE.CT_RAW)
            {
                return fallbackColor;
            }

            var colorBytes = BitConverter.GetBytes(colorReference);
            return Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);
        }
    }
}
