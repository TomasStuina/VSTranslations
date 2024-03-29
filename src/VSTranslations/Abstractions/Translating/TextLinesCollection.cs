﻿using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VSTranslations.Abstractions.Translating
{
    public class TextLinesCollection : IEnumerable<TextLine>
    {
        private readonly LinkedList<TextLine> _lines = new();

        public void Add(TextLine line) => _lines.AddLast(line);

        public IEnumerator<TextLine> GetEnumerator() => _lines.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _lines.GetEnumerator();

        public override string ToString()
        {
            var textBuilder = new StringBuilder();

            foreach (var line in _lines)
            {
                textBuilder.AppendLine(line.Text);
            }

            return textBuilder.ToString();
        }
    }
}
