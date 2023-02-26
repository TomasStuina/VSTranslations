using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Options;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Services.Translating
{
    /// <summary>
    /// Default implementation of <see cref="ITranslatorEngineProvider"/>.
    /// </summary>
    [Export(typeof(ITranslatorEngineProvider))]
    internal class TranslatorEngineProvider : ITranslatorEngineProvider
    {
        private static readonly Lazy<ITranslatorEngine, ITranslatorEngineMetadata> Empty = new Lazy<ITranslatorEngine, ITranslatorEngineMetadata>(() => null, null);
        private IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> _translatorEngines;
        private readonly Func<Task<GeneralOptions>> _generalOptionsAccessor;

        public TranslatorEngineProvider() : this(() => GeneralOptions.GetLiveInstanceAsync())
        {
        }

        internal TranslatorEngineProvider(Func<Task<GeneralOptions>> generalOptionsAccessor)
        {
            _generalOptionsAccessor = generalOptionsAccessor;
        }


        [ImportMany]
        public IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> TranslatorEngines
        {
            get => _translatorEngines ?? Enumerable.Empty<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>>();
            internal set => _translatorEngines = value;
        }

        /// <inheritdoc/>
        public IEnumerable<ITranslatorEngineMetadata> GetAllMetadataEntries()
        {
            return TranslatorEngines.Select(engine => engine.Metadata);
        }

        /// <inheritdoc/>
        public async Task<ITranslatorEngine> GetAsync()
        {
            var descriptor = await GetEngineDescriptorAsync();
            return descriptor?.Value;
        }

        /// <inheritdoc/>
        public async Task<(ITranslatorEngine Engine, ITranslatorEngineMetadata Metadata)> GetWithMetadataAsync()
        {
            var descriptor = await GetEngineDescriptorAsync();
            return (descriptor?.Value, descriptor?.Metadata);
        }

        private async Task<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> GetEngineDescriptorAsync()
        {
            var options = await _generalOptionsAccessor();
            var selectedPluginId = options?.SelectedPluginId;

            Lazy<ITranslatorEngine, ITranslatorEngineMetadata> matchingDescriptor = null;

            if (!string.IsNullOrEmpty(selectedPluginId))
            {
                matchingDescriptor = TranslatorEngines
                    .FirstOrDefault(descriptor => string.Equals(descriptor.Metadata.Id, selectedPluginId, StringComparison.OrdinalIgnoreCase));
            }

            matchingDescriptor ??= TranslatorEngines.FirstOrDefault();

            return matchingDescriptor ?? Empty;
        }
    }
}
