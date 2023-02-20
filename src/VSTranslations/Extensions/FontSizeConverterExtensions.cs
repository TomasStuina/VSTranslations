using System.Windows;
using VSTranslations.Common.Extensions;

namespace VSTranslations.Extensions
{
    /// <summary>
    /// <see cref="FontSizeConverter"/> extensions.
    /// </summary>
    public static class FontSizeConverterExtensions
    {
        /// <summary>
        /// Converts <paramref name="pointSize"/> value to
        /// a proportional point size in WPF.
        /// </summary>
        /// <param name="fontSizeConverter">Converter to use.</param>
        /// <param name="pointSize">Original point size.</param>
        /// <returns>WPF point size.</returns>
        public static double ConvertFromPointSize(this FontSizeConverter fontSizeConverter, uint pointSize) =>
            (double)fontSizeConverter.ThrowIfNull(nameof(fontSizeConverter)).ConvertFrom($"{pointSize}pt");
    }
}
