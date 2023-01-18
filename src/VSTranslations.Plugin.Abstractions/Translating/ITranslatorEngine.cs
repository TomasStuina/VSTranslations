using System.Threading.Tasks;

namespace VSTranslations.Plugin.Abstractions.Translating
{
    public interface ITranslatorEngine
    {
        Task<string> TranslateAsync(string text);
    }
}
