using System.Collections.Generic;
using System.Threading.Tasks;

namespace VSTranslations.Plugin.Abstractions.Translating
{
    /// <summary>
    /// An interface descirbing a translator engine that provides the core
    /// functionality to translate texts.
    /// </summary>
    /// <remarks>
    /// Avoid using this interface directly.
    /// </remarks>
    public interface ITranslatorEngine
    {
        /// <summary>
        /// Translates the provided <paramref name="text"/>.
        /// </summary>
        /// <param name="text">Text to translated.</param>
        /// <returns><see cref="Task{string}"/> indicating the completion with translated text as the result.</returns>
        Task<string> TranslateAsync(string text);

        /// <summary>
        /// Gets the <see cref="ITranslatorEngineConfig"/> associated with this translator engine.
        /// </summary>
        ITranslatorEngineConfig TranslatorEngineConfig { get; }
    }
}
