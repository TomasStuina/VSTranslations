using System.Collections.Generic;
using VSTranslations.Plugin.Abstractions.Translating;

namespace VSTranslations.Abstractions.Translating
{
    public interface ITranslatorEngineProvider
    {
        ITranslatorEngine Get();

        (ITranslatorEngine Engine, ITranslatorEngineMetadata Metadata) GetWithMetadata();

        IEnumerable<ITranslatorEngineMetadata> GetAllMetadataEntries();
    }
}
