using System.ComponentModel;

namespace VSTranslations.Plugin.Abstractions.Translating
{
    public interface ITranslatorEngineMetadata
    {
        string Id { get; }

        string Name { get; }

        [DefaultValue(1)]
        int Version { get; }
    }
}
