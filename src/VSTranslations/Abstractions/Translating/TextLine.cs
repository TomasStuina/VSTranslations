using Microsoft.VisualStudio.Text;

namespace VSTranslations.Abstractions.Translating
{
    public class TextLine
    {
        public TextLine(SnapshotSpan lineSpan, string text)
        {
            LineSpan = lineSpan;
            Text = text;
        }

        public SnapshotSpan LineSpan { get; }

        public string Text { get; set; }
    }
}
