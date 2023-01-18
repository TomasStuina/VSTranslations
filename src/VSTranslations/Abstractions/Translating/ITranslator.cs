using Microsoft.VisualStudio.Text;
using System.Threading.Tasks;

namespace VSTranslations.Abstractions.Translating
{
    public interface ITranslator
    {
        Task<TextLinesCollection> TranslateAsync(SnapshotSpan snapshotSpan);
    }
}
