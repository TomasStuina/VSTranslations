using System.Collections.Generic;
using System.Threading.Tasks;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Abstractions.Translating
{
    /// <summary>
    /// Describes translator engine provider.
    /// </summary>
    public interface ITranslatorEngineProvider
    {
        /// <summary>
        /// Gets currently used <see cref="ITranslatorEngine"/>.
        /// </summary>
        /// <returns><see cref="Task{ITranslatorEngine}"/> indicating the completion with <see cref="ITranslatorEngine"/>.</returns>
        Task<ITranslatorEngine> GetAsync();

        /// <summary>
        /// Gets currently used <see cref="ITranslatorEngine"/> with <see cref="ITranslatorEngineMetadata"/>.
        /// </summary>
        /// <returns><see cref="Task{ITranslatorEngine}"/> indicating the completion with <see cref="ITranslatorEngine"/> and <see cref="ITranslatorEngineMetadata"/>.</returns>
        Task<(ITranslatorEngine Engine, ITranslatorEngineMetadata Metadata)> GetWithMetadataAsync();

        /// <summary>
        /// Gets metadata entries of the all registered translator engines.
        /// </summary>
        /// <returns>An enumerable of <see cref="ITranslatorEngineMetadata">.</returns>
        IEnumerable<ITranslatorEngineMetadata> GetAllMetadataEntries();
    }
}
