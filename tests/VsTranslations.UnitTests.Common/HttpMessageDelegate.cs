using System.Net.Http;
using System.Threading.Tasks;

namespace VSTranslations.UnitTests.Common;

public delegate Task<HttpResponseMessage> HttpMessageDelegate(HttpRequestMessage requestMessage);