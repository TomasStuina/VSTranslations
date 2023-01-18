using System.Collections.Generic;
using System.IO;

namespace VSTranslations.Common.Extensions
{
    /// <summary>
    /// <see cref="string"/> extensions.
    /// </summary>
    public static class StringExtensions
    {
        private const char OpeningBracket = '{';
        private const char ClosingBracket = '}';
        private const char Semicolon = ';';
        private const char Comma = ',';

        /// <summary>
        /// Splits <paramref name="input"/> string value into multiple
        /// line strings where the separator is a new line symbol.
        /// </summary>
        /// <param name="input">String value to split.</param>
        /// <returns><see cref="IEnumerable{string}"/> of split lines.</returns>
        public static IEnumerable<string> SplitToLines(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                yield break;
            }

            using var reader = new StringReader(input);

            string line;
            while ((line = reader.ReadLine()) is not null)
            {
                yield return line;
            }
        }

        /// <summary>
        /// Checks if the given text is '{' string value.
        /// </summary>
        /// <param name="text">String value to check.</param>
        /// <returns><c>true</c> if it is. Otherwise - <c>false</c></returns>
        public static bool IsOpening(this string text)
        {
            return text is not null && text.Length == 1 && text[0] == OpeningBracket;
        }

        /// <summary>
        /// Checks if the given text is '}', ';', or ',' string value.
        /// </summary>
        /// <param name="text">String value to check.</param>
        /// <returns><c>true</c> if it is. Otherwise - <c>false</c></returns>
        public static bool IsClosing(this string text)
        {
            if (text is null || text.Length == 0 || text.Length > 2)
            {
                return false;
            }

            var isClosingBracket = text[0] == ClosingBracket;
            var semicolonOrComma = text.Length == 2 ? text[1] : text[0];

            return isClosingBracket || semicolonOrComma == Semicolon || semicolonOrComma == Comma;
        }
    }
}
