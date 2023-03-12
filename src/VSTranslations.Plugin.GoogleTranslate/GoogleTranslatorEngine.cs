using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VSTranslations.Plugin.Abstractions.Translating;
using System.Net;
using VSTranslations.Plugin.Abstractions.Caching;

namespace VSTranslations.Plugin.GoogleTranslate
{
    [Export(typeof(ITranslatorEngine))]
    [ExportMetadata(nameof(ITranslatorEngineMetadata.Name), ProviderName)]
    [ExportMetadata(nameof(ITranslatorEngineMetadata.Id), Vsix.Id)]
    [ExportMetadata(nameof(ITranslatorEngineMetadata.Version), 1)]
    internal class GoogleTranslatorEngine : TranslatorEngineBase
    {
        private const string TranslationEndpoint = "https://translate.googleapis.com/translate_a/single";
        internal const string ProviderName = "GoogleApis (Free)";

        private readonly HttpClient _httpClient;

        [ImportingConstructor]
        public GoogleTranslatorEngine(ITranslatorEngineConfig<GoogleTranslatorEngine> translatorEngineConfig, ICacheFactory cacheFactory)
            : this(new HttpClientHandler(), translatorEngineConfig, cacheFactory)
        {
        }

        internal GoogleTranslatorEngine(HttpMessageHandler handler, ITranslatorEngineConfig<GoogleTranslatorEngine> translatorEngineConfig, ICacheFactory cacheFactory)
            : base(translatorEngineConfig, cacheFactory)
        {
            _httpClient = new HttpClient(handler);
        }

        protected override async Task<string> TranslateAsync(string text, Language source, Language target)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }

            var encodedText = WebUtility.UrlEncode(text);
            var url = $"{TranslationEndpoint}?client=gtx&sl={source.Code}&tl={target.Code}&hl=en-US&dt=t&dt=bd&dj=1&source=icon&tk=310461.310461&q={encodedText}";

            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            if (!response.IsSuccessStatusCode)
            {
                return text;
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var translationResponse = await JsonSerializer.DeserializeAsync<TranslationResponse>(responseStream);
            var translatedText = GetTranslatedText(translationResponse);

            if (translatedText is null)
            {
                return text;
            }

            return translatedText;
        }

        private static string GetTranslatedText(TranslationResponse translationResponse)
        {
            var translatedSentences = translationResponse?.Sentences;
            if (translatedSentences is null)
            {
                return null;
            }

            var translatedTextBuilder = new StringBuilder();

            foreach (var translatedSentence in translatedSentences)
            {
                translatedTextBuilder.Append(translatedSentence.TranslatedText);
            }

            return translatedTextBuilder.ToString();
        }

        private class TranslationResponse
        {
            [JsonPropertyName("sentences")]
            public IEnumerable<TranslationSentence> Sentences { get; set; }
        }

        private class TranslationSentence
        {
            [JsonPropertyName("trans")]
            public string TranslatedText { get; set; }
        }
    }
}
