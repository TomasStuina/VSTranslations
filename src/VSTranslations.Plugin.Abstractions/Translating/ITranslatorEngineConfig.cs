using System.Collections.Generic;
using System.Threading.Tasks;

namespace VSTranslations.Plugin.Abstractions.Translating;

/// <summary>
/// A generic interface derived from <see cref="ITranslatorEngineConfig"/>.
/// </summary>
/// <typeparam name="TEngine"></typeparam>
public interface ITranslatorEngineConfig<TEngine> : ITranslatorEngineConfig
{
}

/// <summary>
/// An interface for decribing <see cref="ITranslatorEngine"/> configuration.
/// </summary>
public interface ITranslatorEngineConfig
{
    /// <summary>
    /// Get all supported languages asynchronously.
    /// </summary>
    /// <returns><see cref="ValueTask{IReadOnlyList{Language}}"/> indicating the completion with <see cref="IReadOnlyList{Language}"/> as the result.</returns>
    ValueTask<IReadOnlyList<Language>> GetLanguagesAsync();

    /// <summary>
    /// Gets the current source language that is used by the <see cref="ITranslatorEngine"/>.
    /// </summary>
    /// <returns><see cref="ValueTask{IReadOnlyList{Language}}"/> indicating the completion with <see cref="IReadOnlyList{Language}"/> as the result.</returns>
    ValueTask<Language> GetSourceLanguageAsync();

    /// <summary>
    /// Gets the current target language that is used by the <see cref="ITranslatorEngine"/>.
    /// </summary>
    /// <returns><see cref="ValueTask{IReadOnlyList{Language}}"/> indicating the completion with <see cref="IReadOnlyList{Language}"/> as the result.</returns>
    ValueTask<Language> GetTargetLanguageAsync();

    /// <summary>
    /// Sets the current source language to use by the <see cref="ITranslatorEngine"/>.
    /// </summary>
    /// <param name="language">Language to set.</param>
    /// <returns><see cref="ValueTask{IReadOnlyList{Language}}"/> indicating the completion.</returns>
    ValueTask SetSourceLanguageAsync(Language language);

    /// <summary>
    /// Sets the current target language to use by the <see cref="ITranslatorEngine"/>.
    /// </summary>
    /// <param name="language">Language to set.</param>
    /// <returns><see cref="ValueTask{IReadOnlyList{Language}}"/> indicating the completion.</returns>
    ValueTask SetTargetLanguageAsync(Language language);
}