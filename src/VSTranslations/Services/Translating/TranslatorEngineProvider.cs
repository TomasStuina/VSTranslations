using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using VSTranslations.Abstractions.Translating;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Services.Translating
{
    [Export(typeof(ITranslatorEngineProvider))]
    internal class TranslatorEngineProvider : ITranslatorEngineProvider
    {
        private string _translationEngineId = "";
        private IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> _translatorEngines;

        [ImportMany]
        public IEnumerable<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>> TranslatorEngines
        {
            get => _translatorEngines ?? Enumerable.Empty<Lazy<ITranslatorEngine, ITranslatorEngineMetadata>>();
            internal set => _translatorEngines = value;
        }

        public ITranslatorEngine Get()
        {
            return GetEngineDescriptor()?.Value;
        }

        public IEnumerable<ITranslatorEngineMetadata> GetAllMetadataEntries()
        {
            return TranslatorEngines.Select(engine => engine.Metadata);
        }

        public (ITranslatorEngine Engine, ITranslatorEngineMetadata Metadata) GetWithMetadata()
        {
            var plugin = GetEngineDescriptor();
            return (plugin?.Value, plugin?.Metadata);
        }

        private Lazy<ITranslatorEngine, ITranslatorEngineMetadata> GetEngineDescriptor()
        {
            foreach (var descriptor in TranslatorEngines)
            {
                if (string.Equals(descriptor.Metadata.Id, _translationEngineId, StringComparison.OrdinalIgnoreCase))
                {
                    return descriptor;
                }
            }

            return new Lazy<ITranslatorEngine, ITranslatorEngineMetadata>(() => null, null);
        }
    }
}
