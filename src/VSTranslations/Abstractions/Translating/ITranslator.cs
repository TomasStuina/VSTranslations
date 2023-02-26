using Microsoft.VisualStudio.Text;
using System.Threading.Tasks;

namespace VSTranslations.Abstractions.Translating
{
    /// <summary>
    /// Describes the translator that uses a selected translator engine
    /// to translate snapshot spans.
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Translates provided <paramref name="snapshotSpan"/>.
        /// </summary>
        /// <param name="snapshotSpan"><see cref="SnapshotSpan"/> to translate.</param>
        /// <returns><see cref="Task{TextLinesCollection}"/> intdicating the completion with a collection of translated text lines</returns>
        Task<TextLinesCollection> TranslateAsync(SnapshotSpan snapshotSpan);
    }
}
