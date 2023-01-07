using System.Collections.Generic;
using System.IO;

namespace VSTranslations.Extensions
{
    public static class StringExtensions
    {
        private const char OpeningBracket = '{';
        private const char ClosingBracket = '}';
        private const char Semicolon = ';';
        private const char Comma = ',';

        public static IEnumerable<string> SplitToLines(this string input)
        {
            if (input is null)
            {
                yield break;
            }

            using (var reader = new StringReader(input))
            {
                string line;
                while (!((line = reader.ReadLine()) is null))
                {
                    yield return line;
                }
            }
        }

        public static bool IsOpening(this string text)
        {
            return text.Length == 1 && text[0] == OpeningBracket;
        }

        public static bool IsClosing(this string text)
        {
            if (text.Length == 0 || text.Length > 2)
            {
                return false;
            }

            var isClosingBracket = text[0] == ClosingBracket;
            var semicolonOrComma = text.Length == 2 ? text[1] : text[0];

            return isClosingBracket || semicolonOrComma == Semicolon || semicolonOrComma == Comma;
        }
    }
}
